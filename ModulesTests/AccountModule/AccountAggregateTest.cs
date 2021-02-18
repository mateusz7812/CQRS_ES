using System;
using System.Collections.Generic;
using System.Linq;
using AccountModule.Write;
using Core;
using Events;
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
            Assert.Null(accountAggregate.Name);
        }

        [Fact]
        public void TestCreateAccountEvent()
        {
            var accountGuid = Guid.NewGuid();
            var accountName = "TestName";
            var createAccountEvent = new CreateAccountEvent{
                EventGuid = Guid.NewGuid(), 
                ItemGuid = accountGuid, 
                AccountName = accountName};
            var accountAggregate = new AccountAggregate();

            accountAggregate.From(new List<IEvent>() { createAccountEvent });

            Assert.Equal(accountGuid, accountAggregate.Guid);
            Assert.Equal(accountName, accountAggregate.Name);
        }

        [Fact]
        public void TestAddDepositToAccountEvent()
        {
            var accountGuid = Guid.NewGuid();
            var depositId = Guid.NewGuid();
            var eventGuid = Guid.NewGuid();
            var addDepositToAccountEvent = new AddDepositToAccountEvent{
                EventGuid = eventGuid, 
                ItemGuid = accountGuid, 
                DepositId = depositId};
            var accountAggregate = new AccountAggregate();

            accountAggregate.From(new List<IEvent>() { addDepositToAccountEvent });

            Assert.Single(accountAggregate.DepositsGuides);
            Assert.Equal(depositId, accountAggregate.DepositsGuides.First());
        }
    }
}
