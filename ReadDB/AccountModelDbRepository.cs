using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Core;
using Models;
using Optionals;

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

        public Optional<AccountModel> FindById(Guid itemGuid)
        {
            using var ctx = _ctxFactoryMethod.Create(); 
            IQueryable<AccountModel> accounts = ctx.Accounts.Where(m => m.Guid.Equals(itemGuid)).Include(m => m.Deposits);
            return accounts.Any() ? accounts.First() : new Optional<AccountModel> {Code = Codes.NotFound};
        }

        public List<AccountModel> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}