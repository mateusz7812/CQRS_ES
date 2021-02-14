using System;
using EventHandlers.Models;
using EventHandlers.Repositories;
using Xunit;

namespace SqLiteTest
{
    public class SqlLiteTest
    {
        [Fact]
        public void TestFindById()
        {
            var accountGuid = Guid.NewGuid();
            var account = new AccountModule(accountGuid);
            var repository = new AccountSqlLiteRepository("accounts");
            repository.CreateTable();
            repository.Save(account);

            var savedAccount = repository.FindById(accountGuid);

            Assert.Equal(accountGuid, savedAccount.Guid);
        }

        [Fact]
        public void TestFindAll()
        {
            var accountGuid = Guid.NewGuid();
            var account = new AccountModule(accountGuid);
            var repository = new AccountSqlLiteRepository("accounts");
            repository.CreateTable();
            repository.Save(account);

            var accounts = repository.FindAll();

            Assert.Single(accounts);

            var savedAccount = accounts[0];

            Assert.Equal(accountGuid, savedAccount.Guid);
        }
    }
}
