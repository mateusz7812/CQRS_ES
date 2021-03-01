using Core;
using DepositModule.CreateDeposit;
using Events;
using Moq;
using Xunit;

namespace ModulesTests.DepositModule
{
    public class AddDepositToAccountEventHandlerTest
    {
        [Fact]
        public void TestCanHandle()
        {
            var eventHandler = new AddDepositToAccountEventHandler();

            Assert.True(eventHandler.CanHandle(new AddDepositToAccountEvent()));
        }

        [Fact]
        public void TestCanNotHandle()
        {
            var eventHandler = new AddDepositToAccountEventHandler();

            Assert.False(eventHandler.CanHandle(Mock.Of<IEvent>()));
        }
    }
}