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
    public class DepositModelRepository : IModelRepository<DepositModel>
    {
        private readonly IDbContextFactoryMethod<ModelDbContext> _ctxFactoryMethod;

        public DepositModelRepository(IDbContextFactoryMethod<ModelDbContext> ctxFactoryMethod)
        {
            _ctxFactoryMethod = ctxFactoryMethod;
        }

        public void Save(DepositModel item)
        {
            using var ctx = _ctxFactoryMethod.Create();
            var model = ctx.Deposits.Create();
            var accounts = ctx.Accounts.Where(m => m.Guid.Equals(item.Account.Guid));
            if (accounts.Count() != 1) 
                return;
            var account = accounts.First();
            model.Account = account;
            model.Guid = item.Guid;
            ctx.Deposits.AddOrUpdate(model);
            ctx.SaveChanges();
        }

        public Optional<DepositModel> FindById(Guid itemGuid)
        {
            using var ctx = _ctxFactoryMethod.Create();
            return ctx.Deposits.Include(m => m.Account).First(m => m.Guid.Equals(itemGuid));
        }

        public List<DepositModel> FindAll()
        {
            using var ctx = _ctxFactoryMethod.Create();
            return ctx.Deposits.Include(m => m.Account).ToList();
        }

        public void Delete(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}