using System;
using Core;
using DepositModule.CreateDeposit;
using Events;
using Models;
using Moq;
using Xunit;

namespace ModulesTests.DepositModule
{
    public class CreateDepositEventHandlerTest
    {
        [Fact]
        public void TestCanHandle()
        {
            var eventHandler = new CreateDepositEventHandler(Mock.Of<IService<DepositModel>>());
            
            Assert.True(eventHandler.CanHandle(new CreateDepositEvent()));
        }

        [Fact]
        public void TestCanNotHandle()
        {
            var eventHandler = new CreateDepositEventHandler(Mock.Of<IService<DepositModel>>());
            
            Assert.False(eventHandler.CanHandle(Mock.Of<IEvent>()));
        }

        [Fact]
        public void TestHandle()
        {
            var depositGuid = Guid.NewGuid();
            var accountGuid = Guid.NewGuid();
            var eventGuid = Guid.NewGuid();
            var createDepositEvent = new CreateDepositEvent
            {
                ItemGuid = depositGuid,
                AccountGuid = accountGuid,
                EventGuid = eventGuid
            };
            var depositServiceMock = new Mock<IService<DepositModel>>();
            depositServiceMock.Setup(m =>
                m.Save(It.Is<DepositModel>(model => model.Guid == depositGuid && model.Account.Guid == accountGuid)));
            var eventHandler = new CreateDepositEventHandler(depositServiceMock.Object);
            
            eventHandler.Handle(createDepositEvent);

            depositServiceMock.VerifyAll();
        }
    }
}