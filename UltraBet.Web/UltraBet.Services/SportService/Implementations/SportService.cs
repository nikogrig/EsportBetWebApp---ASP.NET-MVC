using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Data;
using UltraBet.Data.Models;
using UltraBet.Services.IdCheckService.Contracts;
using UltraBet.Services.SportService.Contracts;
using UltraBet.Services.SportService.Models;
using static UltraBet.Constants.XmlConstants;


namespace UltraBet.Services.SportService.Implementations
{
    public class SportService : ISportService
    {
        private readonly BetPlusDbContext db;
        private readonly ICheckIdService checkIdService;

        public SportService(BetPlusDbContext db, ICheckIdService checkIdService)
        {
            this.db = db;
            this.checkIdService = checkIdService;
        }

        public void AddSportElement(XElement element)
        {
            if (this.checkIdService.IsElementIdNotExist<Sport>(Convert.ToInt32(element.Attribute(SPORT_ID).Value)))
            {
                var sport = new Sport
                {
                    Id = Convert.ToInt32(element.Attribute(SPORT_ID).Value),
                    Name = element.Attribute(SPORT_NAME).Value,
                };

                this.db.Sports.Add(sport);

                this.db.SaveChanges();
            }
        }

        public IEnumerable<SportServiceModel> GetListOfSports()
        {
            var sports = this.db
                .Sports
                .OrderByDescending(s => s.Id)
                .ProjectTo<SportServiceModel>()
                .ToList();
                       
            return sports;
        }
    }
}
