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
    public class DepositModelRepository: IModelRepository<DepositModel>
    {
        private readonly IDbContextFactoryMethod<ModelDbContext> _ctxFactoryMethod;

        public DepositModelRepository(IDbContextFactoryMethod<ModelDbContext> ctxFactoryMethod)
        {
            _ctxFactoryMethod = ctxFactoryMethod;
        }

        public void Save(DepositModel item)
        {
            using (var ctx = _ctxFactoryMethod.Create())
            {
                var test = ctx.Deposits.Create();
                test.Account = ctx.Accounts.First(m => m.Guid.Equals(item.Account.Guid));
                test.Guid = item.Guid;
                ctx.Deposits.AddOrUpdate(test);
                ctx.SaveChanges();
            }
        }

        public Optional<DepositModel> FindById(Guid itemGuid)
        {
            try
            {
                using var ctx = _ctxFactoryMethod.Create();
                return ctx.Deposits.Include(m => m.Account).First(m => m.Guid.Equals(itemGuid));
            }
            catch (Exception e)
            {
                return Codes.DbError(e.Message);
            }
        }

        public List<DepositModel> FindAll()
        {
            using var ctx = _ctxFactoryMethod.Create();
            return ctx.Deposits.Include(m=>m.Account).ToList();
        }

        public void Delete(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
