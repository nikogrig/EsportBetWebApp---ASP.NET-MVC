using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Services.MatchService.Models;

namespace UltraBet.Services.MatchService.Contracts
{
    public interface IMatchService
    {
        void AddMatchElement(XElement element, string eventId);

        IEnumerable<MatchesServiceModel> GetListOfMatches();

        void DeleteOldElements(DateTime xmlCurrentDateTime);

        void EraseMatchWithoutBets();

        //void EraseMatchMissingMatches();
    }
}
