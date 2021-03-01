using System;
using DepositModule.Write;
using Events;
using Xunit;

namespace ModulesTests.DepositModule
{
    public class DepositAggregateTest
    {
        [Fact]
        public void TestEmptyDeposit()
        {
            var aggregate = new DepositAggregate();
            Assert.Equal(Guid.Empty, aggregate.Guid);
        }

        [Fact]
        public void TestCreateDeposit()
        {
            var aggregate = new DepositAggregate();
            var accountGuid = Guid.NewGuid();
            var depositGuid = Guid.NewGuid();
            var eventGuid = Guid.NewGuid();
            var createDepositEvent = new CreateDepositEvent
            {
                AccountGuid = accountGuid,
                ItemGuid = depositGuid,
                EventGuid = eventGuid
            };
            
            aggregate.Apply(createDepositEvent);

            Assert.Equal(depositGuid, aggregate.Guid);
            Assert.Equal(accountGuid, aggregate.AccountGuid);
        }
    }
}