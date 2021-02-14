using System;
using System.Collections.Generic;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Core;
using Xunit;

namespace AccountModule.Tests
{
    public class AccountTest
    {
        [Fact]
        public void TestCreateAccount()
        {
            var accountGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            var account = new AccountAggregate();
            account.From(new List<IEvent>() { createAccountEvent });

            Assert.Equal(accountGuid, account.Guid);
        }
    }
}
