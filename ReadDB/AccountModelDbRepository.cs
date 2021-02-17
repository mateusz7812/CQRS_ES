using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using AccountModule.Read;
using Core;

namespace ReadDB
{
    class AccountModelDbRepository : IModelRepository<AccountModel>
    {
        public void Save(AccountModel item)
        {
            using var ctx = new ModelDbContext();
            {
                ctx.Accounts.AddOrUpdate(item);
                ctx.SaveChanges();
            }
        }

        public AccountModel FindById(Guid itemGuid)
        {
            using var ctx = new ModelDbContext();
            return ctx.Accounts.Where(m => m.Guid.Equals(itemGuid)).Include(m => m.Deposits).First();
        }

        public List<AccountModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}