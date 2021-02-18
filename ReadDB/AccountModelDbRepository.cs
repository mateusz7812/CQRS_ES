using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Core;
using Models;

namespace ReadDB
{
    public class AccountModelDbRepository : IModelRepository<AccountModel>
    {
        private readonly IDbContextFactoryMethod<ModelDbContext> _ctxFactoryMethod;
        public AccountModelDbRepository(IDbContextFactoryMethod<ModelDbContext> ctxFactoryMethod)
        {
            _ctxFactoryMethod = ctxFactoryMethod;
        }

        public void Save(AccountModel item)
        {
            using var ctx = _ctxFactoryMethod.Create();
            {
                ctx.Accounts.AddOrUpdate(item);
                ctx.SaveChanges();
            }
        }

        public AccountModel FindById(Guid itemGuid)
        {
            using var ctx = _ctxFactoryMethod.Create();
            return ctx.Accounts.Where(m => m.Guid.Equals(itemGuid)).Include(m => m.Deposits).First();
        }

        public List<AccountModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}