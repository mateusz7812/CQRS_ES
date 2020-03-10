using CommandHandlers;
using CommandHandlers.Aggregates;
using CommandHandlers.AggregatesServices;
using CommandHandlers.CommandHandlers;
using EventsAndCommands.Commands;
using EventsAndCommands.Events;
using Moq;
using System;
using EventsAndCommands;
using Xunit;

namespace CommandHandlersTests
{
    public class CreateAccountCommandHandlerTest
    {
        [Fact]
        public void TestCommandType()
        {
            var accountServiceMock = new Mock<IAggregateService<Account>>();
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);

            var commandType = commandHandler.CommandType;
            Assert.True(commandType == typeof(CreateAccountCommand));
        }

        [Fact]
        public void TestVerify()
        {
            var accountServiceMock = new Mock<IAggregateService<Account>>();
            accountServiceMock.Setup(m => m.SaveAndPublish(It.IsAny<IEvent>()));
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            var accountGuid = Guid.NewGuid();
            ICommand command = new CreateAccountCommand(accountGuid);

            var commandIsCorrect = commandHandler.CommandIsCorrect(command);

            Assert.True(commandIsCorrect);
        }

        [Fact]
        public void TestVerifyBad()
        {
            var accountServiceMock = new Mock<IAggregateService<Account>>();
            accountServiceMock.Setup(m => m.SaveAndPublish(It.IsAny<IEvent>()));
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            ICommand command = new CreateAccountCommand(Guid.Empty);

            var commandIsCorrect = commandHandler.CommandIsCorrect(command);

            Assert.False(commandIsCorrect);
        }

        [Fact]
        public void TestHandle()
        {
            var accountServiceMock = new Mock<IAggregateService<Account>>();
            accountServiceMock.Setup(m => m.SaveAndPublish(It.IsAny<IEvent>()));
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            var accountGuid = Guid.NewGuid();
            ICommand command = new CreateAccountCommand(accountGuid);

            commandHandler.Handle(command);

            Assert.Equal(1, accountServiceMock.Invocations.Count);
            Assert.Equal(accountGuid, (accountServiceMock.Invocations[0].Arguments[0] as CreateAccountEvent).AccountGuid);
        }
    }
}
