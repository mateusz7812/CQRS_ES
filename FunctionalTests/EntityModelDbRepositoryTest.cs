using System;
using System.Linq;
using Models;
using Optionals;
using ReadDB;
using Xunit;

namespace FunctionalTests
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
            AccountModel accountModel = accountModelDbRepository.FindById(accountToSave.Guid);
            Assert.Equal(accountToSave.Guid, accountModel.Guid);
            Assert.Equal(accountToSave.Name, accountModel.Name);
            var depositModelRepository = new DepositModelRepository(ctxFactoryMethod);
            var depositToSave = new DepositModel
            {
                Account = accountModel,
                Guid = Guid.NewGuid()
            };
            depositModelRepository.Save(depositToSave);
            DepositModel depositModel = depositModelRepository.FindById(depositToSave.Guid);
            Assert.Equal(depositToSave.Guid, depositModel.Guid);
            Assert.Equal(depositToSave.Account.Guid, depositModel.Account.Guid);
            accountModel = accountModelDbRepository.FindById(accountToSave.Guid);
            Assert.Single(accountModel.Deposits);
            Assert.Equal(depositToSave.Guid, accountModel.Deposits.First().Guid);
        }

        [Fact]
        public void TestGetNotExistingAccount()
        {
            var ctxFactoryMethod = new SqLiteCtxFactoryMethod();
            var accountModelDbRepository = new AccountModelDbRepository(ctxFactoryMethod);

            Optional<AccountModel> accountModelOptional = accountModelDbRepository.FindById(Guid.NewGuid());
            
            Assert.Same(Codes.NotFound, accountModelOptional.Code);
            Assert.Throws<Exception>(() => accountModelOptional.Item);
        }
    }
}