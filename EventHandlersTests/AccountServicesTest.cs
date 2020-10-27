using System;
using System.Collections.Generic;
using System.Text;
using EventHandlers;
using EventHandlers.Models;
using EventHandlers.Repositories;
using EventHandlers.Services;
using Moq;
using Xunit;

namespace EventHandlersTests
{
    public class AccountServicesTest
    {
        [Fact]
        public void CreateAccountTest()
        {
            var accountId = Guid.NewGuid();
            var account = new Account(accountId);
            var repository = new AccountSqlLiteRepository("account");
            repository.CreateTable();

            var accountService = new AccountService(repository);

            accountService.Save(account);
            var foundAccount = accountService.FindById(accountId);

            Assert.True(foundAccount.Guid.Equals(accountId));
        }

    }
}
