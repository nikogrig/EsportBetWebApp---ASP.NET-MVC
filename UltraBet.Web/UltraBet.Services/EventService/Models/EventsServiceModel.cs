using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Common.Mapping;
using UltraBet.Data.Models;

namespace UltraBet.Services.EventService.Models
{
    public class EventsServiceModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Game { get; set; }

        public string Legue { get; set; }

        public bool IsLive { get; set; }

        public int CategoryId { get; set; }

        public int SportId { get; set; }
    }
}
