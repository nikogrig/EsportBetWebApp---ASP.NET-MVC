using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Services.OddService.Models;

namespace UltraBet.Services.OddService.Contracts
{
    public interface IOddService
    {
        void AddOrUpdateOddElement(XElement element, string betId);

        IEnumerable<OddsServiceModel> GetListOfOdds();
    }
}
