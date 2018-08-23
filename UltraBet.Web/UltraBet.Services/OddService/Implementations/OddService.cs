using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UltraBet.Data;
using UltraBet.Data.Models;
using UltraBet.Services.IdCheckService.Contracts;
using UltraBet.Services.OddService.Contracts;
using UltraBet.Services.OddService.Models;
using static UltraBet.Constants.XmlConstants;

namespace UltraBet.Services.OddService.Implementations
{
    public class OddService : IOddService
    {
        private readonly BetPlusDbContext db;
        private readonly ICheckIdService checkIdService;

        public OddService(BetPlusDbContext db, ICheckIdService checkIdService)
        {
            this.db = db;
            this.checkIdService = checkIdService;
        }

        public void AddOrUpdateOddElement(XElement element, string betId)
        {
            if (checkIdService.IsElementIdNotExist<Odd>(Convert.ToInt32(element.Attribute(ODD_ID).Value)))
            {
                this.AddOddElement(element, betId);
            }
            else
            {
                this.UpdateOddElement(element);
            }
        }

        public IEnumerable<OddsServiceModel> GetListOfOdds()
        {
            var odds = this.db
                .Odds
                .OrderBy(o => o.Id)
                .ProjectTo<OddsServiceModel>()
                .ToList();

            return odds;
        }

        private void AddOddElement(XElement element, string betId)
        {
            var odd = new Odd
            {
                Id = Convert.ToInt32(element.Attribute(ODD_ID).Value),
                Name = element.Attribute(ODD_NAME).Value,
                Value = Convert.ToDouble(element.Attribute(ODD_VALUE).Value),
                SpecialBetValue = element.Attribute(ODD_SPECIAL_BET_VALUE)?.Value,
                BetId = Convert.ToInt32(betId)
            };

            this.db.Odds.Add(odd);

            this.db.SaveChanges();
        }

        private void UpdateOddElement(XElement element)
        {
            var odd = this.db
                .Odds
                .Where(o => o.Id == Convert.ToInt32(element.Attribute(ODD_ID).Value))
                .FirstOrDefault();

            if (odd.Value != Convert.ToDouble(element.Attribute(ODD_VALUE).Value))
            {
                odd.Value = Convert.ToDouble(element.Attribute(ODD_VALUE).Value);

                this.db.SaveChanges();
            }
        }
    }
}
