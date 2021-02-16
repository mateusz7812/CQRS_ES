using System;
using System.Collections.Generic;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Core;
using Xunit;

namespace ModulesTests.AccountModule
{
    public class AccountAggregateTest
    {
        [Fact]
        public void TestEmptyAccount()
        {
            var accountAggregate = new AccountAggregate();

            accountAggregate.From(new List<IEvent>());

            Assert.Equal(Guid.Empty, accountAggregate.Guid);
        }

        [Fact]
        public void TestCreateAccountEvent()
        {
            var accountGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            var accountAggregate = new AccountAggregate();

            accountAggregate.From(new List<IEvent>() { createAccountEvent });

            Assert.Equal(accountGuid, accountAggregate.Guid);
        }
    }
}
