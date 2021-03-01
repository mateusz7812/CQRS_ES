using System;
using Commands;
using Core;
using DepositModule.Transfer;
using Moq;
using Xunit;

namespace ModulesTests.DepositModule
{
    public class TransferCommandHandlerTests
    {
        [Fact]
        public void TestCanHandle()
        {
            var handler = new TransferCommandHandler();
            var command = new TransferCommand
            {
                Currency = Currencies.Dollars.Of(10),
                SourceDepositId = Guid.NewGuid(),
                DestinationDepositId = Guid.NewGuid()
            };

            var canHandle = handler.CanHandle(command);

            Assert.True(canHandle);
        }

        [Fact]
        public void TestCanHandleFalse()
        {
            var handler = new TransferCommandHandler();
            var command = Mock.Of<ICommand>();

            var canHandle = handler.CanHandle(command);

            Assert.False(canHandle);
        }

        [Fact]
        public void TestHandleTransferFromDepositToDeposit()
        {
            var publisher = new Mock<IEventPublisher>();
            var handler = new TransferCommandHandler();
            var command = new TransferCommand
            {
                Currency = Currencies.Dollars.Of(10),
                SourceDepositId = Guid.NewGuid(),
                DestinationDepositId = Guid.NewGuid()
            };

            handler.Handle(command);
            Assert.False(true);
        }
    }
}
