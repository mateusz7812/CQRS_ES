using System;
using System.Collections.Generic;
using System.Linq;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Core;
using DepositModule.CreateDeposit;
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

        [Fact]
        public void TestAddDepositToAccountEvent()
        {
            var accountGuid = Guid.NewGuid();
            var depositId = Guid.NewGuid();
            var eventGuid = Guid.NewGuid();
            var addDepositToAccountEvent = new AddDepositToAccountEvent(eventGuid, accountGuid, depositId);
            var accountAggregate = new AccountAggregate();

            accountAggregate.From(new List<IEvent>() { addDepositToAccountEvent });

            Assert.Single(accountAggregate.DepositsGuides);
            Assert.Equal(depositId, accountAggregate.DepositsGuides.First());
        }
    }
}
