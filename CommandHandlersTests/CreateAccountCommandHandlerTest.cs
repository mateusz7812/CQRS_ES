using CommandHandlers;
using CommandHandlers.Aggregates;
using CommandHandlers.AggregatesServices;
using CommandHandlers.CommandHandlers;
using EventsAndCommands.Events;
using Moq;
using System;
using Commands.Commands;
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
        public void TestCommandCheck()
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
        public void TestIncorrectCommandCheck()
        {
            var accountServiceMock = new Mock<IAggregateService<Account>>();
            accountServiceMock.Setup(m => m.SaveAndPublish(It.IsAny<IEvent>()));
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            ICommand command = new CreateAccountCommand(Guid.Empty);

            var commandIsCorrect = commandHandler.CommandIsCorrect(command);

            Assert.False(commandIsCorrect);
        }

        [Fact]
        public void TestCommandHandle()
        {
            var observerMock = new Mock<IObserver>();
            observerMock.Setup(o => o.Update(It.IsAny<CreateAccountEvent>()));

            var accountServiceMock = new Mock<IAggregateService<Account>>();
            accountServiceMock.Setup(m => m.SaveAndPublish(It.IsAny<IEvent>()));
            
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            commandHandler.AddObserver(observerMock.Object);
            
            var accountGuid = Guid.NewGuid();
            ICommand command = new CreateAccountCommand(accountGuid);

            commandHandler.Handle(command);

            Assert.Equal(1, accountServiceMock.Invocations.Count);
            Assert.Equal(accountGuid, ((CreateAccountEvent) accountServiceMock.Invocations[0].Arguments[0]).AccountGuid);
            observerMock.VerifyAll();
        }
    }
}
