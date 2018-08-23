using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Common.Mapping;
using UltraBet.Data.Models;

namespace UltraBet.Services.BetService.Models
{
    public class BetsServiceModel : IMapFrom<Bet>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public int MatchId { get; set; }
    }
}
