using System;
using EventHandlers.Models;
using EventHandlers.Repositories;
using Xunit;

namespace SqLiteTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var accountGuid = Guid.NewGuid();
            var account = new Account(accountGuid);
            var repository = new AccountSqlLiteRepository("accounts");
            repository.CreateTable();
            repository.Save(account);

            var savedAccount = repository.FindById(accountGuid);

            Assert.Equal(accountGuid, savedAccount.Guid);
        }
    }
}
