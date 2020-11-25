using System;
using System.Collections.Generic;
using CommandHandlers.AccountComponents;
using Events;
using Events.Events;
using Xunit;

namespace AggregatesTests
{
    public class AccountTest
    {
        [Fact]
        public void TestCreateAccount()
        {
            var accountGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            var account = new Account();
            account.From(new List<IEvent>() {createAccountEvent});

            Assert.Equal(accountGuid, account.Guid);
        }
    }
}
