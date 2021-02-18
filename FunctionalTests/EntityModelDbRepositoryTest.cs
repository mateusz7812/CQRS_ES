using System;
using System.Linq;
using System.Threading.Tasks;
using FunctionalTests;
using Models;
using Xunit;

namespace ReadDB
{
    public class EntityModelDbRepositoryTest
    {
        [Fact]
        public void TestSaveAndRead()
        {
            var ctxFactoryMethod = new SqLiteCtxFactoryMethod();
            var accountModelDbRepository = new AccountModelDbRepository(ctxFactoryMethod);
            var accountToSave = new AccountModel
            {
                Guid = Guid.NewGuid(), 
                Name = "TestName"
            };
            accountModelDbRepository.Save(accountToSave);
            var accountModel = accountModelDbRepository.FindById(accountToSave.Guid);
            Assert.Equal(accountToSave.Guid, accountModel.Guid);
            Assert.Equal(accountToSave.Name, accountModel.Name);
            var depositModelRepository = new DepositModelRepository(ctxFactoryMethod);
            var depositToSave = new DepositModel
            {
                Account = accountModel,
                Guid = Guid.NewGuid()
            };
            depositModelRepository.Save(depositToSave);
            var depositModel = depositModelRepository.FindById(depositToSave.Guid);
            Assert.Equal(depositToSave.Guid, depositModel.Guid);
            Assert.Equal(depositToSave.Account.Guid, depositModel.Account.Guid);
            accountModel = accountModelDbRepository.FindById(accountToSave.Guid);
            Assert.Single(accountModel.Deposits);
            Assert.Equal(depositToSave.Guid, accountModel.Deposits.First().Guid);            
        }
    }
}