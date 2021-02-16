using System;
using AccountModule.Read;
using Xunit;

namespace ModulesTests.AccountModule
{
    public class SqlLiteTest
    {
        [Fact]
        public void TestFindById()
        {
            var accountGuid = Guid.NewGuid();
            var account = new AccountModel(accountGuid);
            var repository = new AccountSqlLiteModelRepository("accounts");
            repository.CreateTable();
            repository.Save(account);

            var savedAccount = repository.FindById(accountGuid);

            Assert.Equal(accountGuid, savedAccount.Guid);
        }

        [Fact]
        public void TestFindAll()
        {
            var accountGuid = Guid.NewGuid();
            var account = new AccountModel(accountGuid);
            var repository = new AccountSqlLiteModelRepository("accounts");
            repository.CreateTable();
            repository.Save(account);

            var accounts = repository.FindAll();

            Assert.Single(accounts);

            var savedAccount = accounts[0];

            Assert.Equal(accountGuid, savedAccount.Guid);
        }
    }
}
