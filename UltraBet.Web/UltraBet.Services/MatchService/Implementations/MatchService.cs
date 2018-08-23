using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Data;
using UltraBet.Data.Enums;
using UltraBet.Data.Models;
using UltraBet.Services.IdCheckService.Contracts;
using UltraBet.Services.MatchService.Contracts;
using UltraBet.Services.MatchService.Models;
using static UltraBet.Constants.XmlConstants;

namespace UltraBet.Services.MatchService.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly BetPlusDbContext db;
        private readonly ICheckIdService checkIdService;

        public MatchService(BetPlusDbContext db, ICheckIdService checkIdService)
        {
            this.db = db;
            this.checkIdService = checkIdService;
        }

        public void AddMatchElement(XElement element, string eventId)
        {
            if (this.checkIdService.IsElementIdNotExist<Match>(Convert.ToInt32(element.Attribute(MATCH_ID).Value)))
            {
               var teams = element.Attribute(MATCH_NAME).Value.Split(" - ").ToArray();

                var match = new Match
                {
                    Id = Convert.ToInt32(element.Attribute(MATCH_ID).Value),
                    TeamA = teams[0],
                    TeamB = teams[1],
                    StartDate = Convert.ToDateTime(element.Attribute(MATCH_START_DATE).Value),
                    MatchType = (MatchType)Enum.Parse(typeof(MatchType), element.Attribute(MATCH_TYPE).Value),
                    EventId = Convert.ToInt32(eventId)
                };

                this.db.Matches.Add(match);

                this.db.SaveChanges();
            }
        }

        public void DeleteOldElements(DateTime xmlCurrentDateTime)
        {
            var matches = this.db
                 .Matches
                 .Where(m => m.StartDate < xmlCurrentDateTime)
                 .ToList();         

            foreach (var match in matches)
            {
                if (match != null)
                {
                    this.db.Matches.Remove(match);

                    this.db.SaveChanges();
                }
            }
        }

        public void EraseMatchWithoutBets()
        {
            var removedMatches = this.db.Matches.Where(e => e.Bets.Count() == 0).ToList();

            this.db.RemoveRange(removedMatches);

            this.db.SaveChanges();
        }

        public IEnumerable<MatchesServiceModel> GetListOfMatches()
        {
            var matches = this.db
                .Matches
                .OrderBy(m => m.StartDate)
                .ProjectTo<MatchesServiceModel>()
                .ToList();

            return matches;
        }
    }
}
