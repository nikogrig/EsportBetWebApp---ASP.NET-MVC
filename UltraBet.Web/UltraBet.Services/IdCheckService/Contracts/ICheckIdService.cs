using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraBet.Services.IdCheckService.Contracts
{
    public interface ICheckIdService
    {
        bool IsElementIdNotExist<T>(int id) where T : class;
    }
}
