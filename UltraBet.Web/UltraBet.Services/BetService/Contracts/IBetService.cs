using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Services.BetService.Models;

namespace UltraBet.Services.BetService.Contracts
{
    public interface IBetService
    {
        void AddBetElement(XElement element, string matchId);

        IEnumerable<BetsServiceModel> GetListOfBets();

        void EraseBetWithoutOdds();
    }
}
