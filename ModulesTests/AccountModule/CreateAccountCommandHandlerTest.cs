using System;
using AccountModule.CreateAccount;
using AccountModule.Write;
using Commands;
using Core;
using Events;
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
            var accountName = "testName";
            ICommand command = new CreateAccountCommand{
                Name = accountName
            };

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
            var accountName = "testName";
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<CreateAccountEvent>())).Callback((IEvent e) =>
            {
                Assert.IsType<CreateAccountEvent>(e);
                CreateAccountEvent cae = (CreateAccountEvent) e;
                Assert.Equal(accountName, cae.AccountName);
                Assert.NotEqual(Guid.Empty, cae.ItemGuid);
                Assert.NotEqual(Guid.Empty, cae.EventGuid);
            });
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object, eventPublisherMock.Object);
            ICommand command = new CreateAccountCommand{
                Name = accountName
            };

            commandHandler.Handle((CreateAccountCommand) command);

            eventPublisherMock.VerifyAll();
        }
    }
}