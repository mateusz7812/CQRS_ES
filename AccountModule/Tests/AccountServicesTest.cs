using System;
using AccountModule.Read;
using Xunit;

namespace AccountModule.Tests
{
    public class AccountServicesTest
    {
        [Fact]
        public void CreateAccountTest()
        {
            var accountId = Guid.NewGuid();
            var account = new AccountModel(accountId);
            var repository = new AccountSqlLiteRepository("account");
            repository.CreateTable();

            var accountService = new AccountService(repository);

            accountService.Save(account);
            var foundAccount = accountService.FindById(accountId);

            Assert.True(foundAccount.Guid.Equals(accountId));
        }

    }
}
