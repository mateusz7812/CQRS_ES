using System;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Core;
using Moq;
using Xunit;

namespace AccountModule.Tests
{
    public class CreateAccountCommandHandlerTest
    {

        [Fact]
        public void TestCanHandle()
        {
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            ICommand command = new CreateAccountCommand(Guid.Empty);

            var canHandle = commandHandler.CanHandle(command);

            Assert.True(canHandle);
        }

        [Fact]
        public void TestIncorrectCommandCheck()
        {
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            ICommand command = new Mock<ICommand>().Object;

            var canHandle = commandHandler.CanHandle(command);

            Assert.False(canHandle);
        }

        [Fact]
        public void TestCommandHandle()
        {
            var observerMock = new Mock<IObserver<IEvent>>();
            observerMock.Setup(o => o.OnNext(It.IsAny<CreateAccountEvent>()));

            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            accountServiceMock.Setup(m => m.SaveAndPublish(It.IsAny<IEvent>()));

            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object);
            commandHandler.AddObserver(observerMock.Object);

            var accountGuid = Guid.NewGuid();
            ICommand command = new CreateAccountCommand(accountGuid);

            commandHandler.Handle((CreateAccountCommand)command);

            Assert.Equal(1, accountServiceMock.Invocations.Count);
            Assert.Equal(accountGuid, ((CreateAccountEvent)accountServiceMock.Invocations[0].Arguments[0]).ItemGuid);
            observerMock.VerifyAll();
        }
    }
}
