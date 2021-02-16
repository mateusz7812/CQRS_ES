using System;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Core;
using Moq;
using Xunit;

namespace ModulesTests.AccountModule
{
    public class CreateAccountCommandHandlerTest
    {

        [Fact]
        public void TestCanHandle()
        {
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object, eventPublisherMock.Object);
            ICommand command = new CreateAccountCommand();

            var canHandle = commandHandler.CanHandle(command);

            Assert.True(canHandle);
        }

        [Fact]
        public void TestCanHandleFalse()
        {
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object, eventPublisherMock.Object);
            ICommand command = new Mock<ICommand>().Object;

            var canHandle = commandHandler.CanHandle(command);

            Assert.False(canHandle);
        }

        [Fact]
        public void TestCommandHandle()
        {
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            
            var eventPublisherMock = new Mock<IEventPublisher>();

            eventPublisherMock.Setup(m => m.Publish(It.IsAny<CreateAccountEvent>()));

            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object, eventPublisherMock.Object);
            
            ICommand command = new CreateAccountCommand();

            commandHandler.Handle((CreateAccountCommand)command);

            Assert.Equal(1, eventPublisherMock.Invocations.Count);
            eventPublisherMock.VerifyAll();
        }

    }
}
