using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Data;
using UltraBet.Data.Models;
using UltraBet.Services.EventService.Contracts;
using UltraBet.Services.EventService.Models;
using UltraBet.Services.IdCheckService.Contracts;
using static UltraBet.Constants.XmlConstants;

namespace UltraBet.Services.EventService.Implementations
{
    public class EventService : IEventService
    {
        private readonly BetPlusDbContext db;
        private readonly ICheckIdService checkIdService;

        public EventService(BetPlusDbContext db, ICheckIdService checkIdService)
        {
            this.db = db;
            this.checkIdService = checkIdService;
        }

        public void AddEventElement(XElement element, string sportId)
        {
            if (checkIdService.IsElementIdNotExist<Event>(Convert.ToInt32(element.Attribute(EVENT_ID).Value)))
            {

                var eventName = element.Attribute(EVENT_NAME).Value.Split(", ").ToArray();


                var @event = new Event
                {
                    Id = Convert.ToInt32(element.Attribute(EVENT_ID).Value),
                    Game = eventName[0],
                    Legue = eventName[1],
                    CategoryId = Convert.ToInt32(element.Attribute(EVENT_CATEGORY_ID).Value),
                    SportId = Convert.ToInt32(sportId)
                };

                this.IsLive(element, @event);

                this.db.Events.Add(@event);

                this.db.SaveChanges();
            }
        }

        public void EraceEventsWithoutMatch()
        {
           var removedEvents = this.db.Events.Where(e => e.Matches.Count() == 0).ToList();

            this.db.RemoveRange(removedEvents);

            this.db.SaveChanges();
        }

        public IEnumerable<EventsServiceModel> GetListOfEvents()
        {
            var events = this.db
                .Events
                .OrderBy(e => e.Matches
                               .OrderBy(m =>  m.StartDate)
                               .Select(h => h.StartDate)
                               .FirstOrDefault())
                .ProjectTo<EventsServiceModel>()
                .ToList();

            return events;
        }

        private void IsLive(XElement element, Event @event)
        {
            bool isLive = element.Attribute(EVENT_ISLIVE).Value == "true";

            isLive = true ? @event.IsLive = true : @event.IsLive = false;
        }
    }
}
