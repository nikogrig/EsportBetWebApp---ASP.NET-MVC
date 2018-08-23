using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Common.Mapping;
using UltraBet.Data.Models;

namespace UltraBet.Services.OddService.Models
{
    public class OddsServiceModel : IMapFrom<Odd>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public string SpecialBetValue { get; set; }

        public int BetId { get; set; }
    }
}
