using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Services.EventService.Models;

namespace UltraBet.Services.EventService.Contracts
{
    public interface IEventService
    {
        void AddEventElement(XElement element, string sportId);

        IEnumerable<EventsServiceModel> GetListOfEvents();

        void EraceEventsWithoutMatch();
    }
}
