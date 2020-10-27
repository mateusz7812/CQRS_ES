using System;
using Xunit;
using CommandBus;
using CommandHandlers.CommandHandlers;
using Moq;
using CommandHandlers;
using Commands.Commands;
using EventsAndCommands;

namespace CommandBusTest
{
    public class CommandBusTest
    {
        [Fact]
        public void TestBasic()
        {
            var accountGuid = Guid.NewGuid();
            var command = new CreateAccountCommand(accountGuid);
            var commandBus = new DefaultCommandBus();
            var commandHandlerMock = new Mock<CreateAccountCommandHandler>();
            commandHandlerMock.Setup(m => m.CommandIsCorrect(It.IsAny<ICommand>())).Returns(true);
            commandHandlerMock.Setup(m => m.Handle(It.Is<CreateAccountCommand>(g=>g.AccountGuid.Equals(accountGuid))));
            commandBus.AddCommandHandler(commandHandlerMock.Object);

            commandBus.AddCommand(command);
            commandBus.HandleNext();
            
            commandHandlerMock.VerifyAll();
        }
    }
}
