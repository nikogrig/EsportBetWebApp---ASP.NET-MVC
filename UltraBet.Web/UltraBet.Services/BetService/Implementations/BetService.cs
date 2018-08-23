using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Data;
using UltraBet.Data.Models;
using UltraBet.Services.BetService.Contracts;
using UltraBet.Services.BetService.Models;
using UltraBet.Services.IdCheckService.Contracts;
using static UltraBet.Constants.XmlConstants;

namespace UltraBet.Services.BetService.Implementations
{
    public class BetService : IBetService
    {
        private readonly BetPlusDbContext db;
        private readonly ICheckIdService checkIdService;

        public BetService(BetPlusDbContext db, ICheckIdService checkIdService)
        {
            this.db = db;
            this.checkIdService = checkIdService;
        }

        public void AddBetElement(XElement element, string matchId)
        {
            if (checkIdService.IsElementIdNotExist<Bet>(Convert.ToInt32(element.Attribute(BET_ID).Value)))
            {
                var bet = new Bet
                {
                    Id = Convert.ToInt32(element.Attribute(BET_ID).Value),
                    Name = element.Attribute(BET_NAME).Value,
                    MatchId = Convert.ToInt32(matchId)
                };

                this.IsLive(element, bet);

                this.db.Bets.Add(bet);

                this.db.SaveChanges();
            }
        }

        public void EraseBetWithoutOdds()
        {
            var removedBets = this.db.Bets.Where(e => e.Odds.Count() == 0).ToList();

            this.db.RemoveRange(removedBets);

            this.db.SaveChanges();
        }

        public IEnumerable<BetsServiceModel> GetListOfBets()
        {
            var bets = this.db
                .Bets
                .OrderBy(m => m.Id)
                .ProjectTo<BetsServiceModel>()
                .ToList();

            return bets;
        }

        private void IsLive(XElement element, Bet bet)
        {
            bool isLive = element.Attribute(BET_ISLIVE).Value == "true";

            isLive = true ? bet.IsLive = true : bet.IsLive = false;
        }
    }
}