using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Common.Mapping;
using UltraBet.Data.Enums;
using UltraBet.Data.Models;

namespace UltraBet.Services.MatchService.Models
{
    public class MatchesServiceModel : IMapFrom<Match>
    {
        public int Id { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public DateTime StartDate { get; set; }

        public MatchType MatchType { get; set; }

        public int EventId { get; set; }
    }
}
