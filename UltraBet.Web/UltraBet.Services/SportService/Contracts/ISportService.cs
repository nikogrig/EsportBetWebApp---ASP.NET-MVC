using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraBet.Services.SportService.Models;

namespace UltraBet.Services.SportService.Contracts
{
    public interface ISportService
    {
        void AddSportElement(XElement element);

        IEnumerable<SportServiceModel> GetListOfSports();
    }
}
