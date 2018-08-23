using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UltraBet.Common.Mapping;
using UltraBet.Data.Models;

namespace UltraBet.Services.SportService.Models
{
    public class SportServiceModel : IMapFrom<Sport>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
           // mapper
           //     .CreateMap<Sport, SportServiceModel>()
           //     .ForMember(a => a.Matches, cfq => cfq.MapFrom(a => a.Events.Where(s => s.SportId == this.Id).SelectMany(e => e.Matches.Select(m => m))));
        }
    }
}
