using System;
using System.Collections.Generic;
using System.Text;
using Commands;
using Core;
using Currencies;
using Moq;
using TransferModule.Transfer;
using Xunit;

namespace ModulesTests.TransferModule
{
    public class TransferCommandHandlerTests
    {
        [Fact]
        public void TestCanHandle()
        {
            var handler = new TransferCommandHandler();
            var command = new TransferCommand
            {
                Currency = Mock.Of<ICurrency>(),
                SourceOfTransaction = Mock.Of<TransferCommand.ISideOfTransaction>(),
                DestinationOfTransaction = Mock.Of<TransferCommand.ISideOfTransaction>()
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
                Currency = Mock.Of<ICurrency>(),
                SourceOfTransaction = Mock.Of<TransferCommand.ISideOfTransaction>(),
                DestinationOfTransaction = Mock.Of<TransferCommand.ISideOfTransaction>()
            };

            handler.Handle(command);
            
        }
    }
}
