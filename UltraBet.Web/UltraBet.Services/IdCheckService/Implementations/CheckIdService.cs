using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Data;
using UltraBet.Services.IdCheckService.Contracts;

namespace UltraBet.Services.IdCheckService.Implementations
{
    public class CheckIdService : ICheckIdService
    {
        private readonly BetPlusDbContext db;

        public CheckIdService(BetPlusDbContext db)
        {
            this.db = db;
        }

        public bool IsElementIdNotExist<T>(int id) where T : class
        {
            var searchedObjectById = this.db.Find<T>(id);

            if (searchedObjectById != null)
            {
                return false;
            }

            return true;
        }
    }
}
