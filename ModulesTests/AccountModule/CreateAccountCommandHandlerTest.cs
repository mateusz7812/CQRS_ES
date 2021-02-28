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
            accountServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns((Guid guid) => new AccountAggregate());
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<CreateAccountEvent>())).Callback((IEvent e) =>
            {
                Assert.IsType<CreateAccountEvent>(e);
                CreateAccountEvent createAccountEvent = (CreateAccountEvent) e;
                Assert.Equal(accountName, createAccountEvent.AccountName);
                Assert.NotEqual(Guid.Empty, createAccountEvent.ItemGuid);
                Assert.Equal(Guid.Empty, createAccountEvent.EventGuid);
            });
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object, eventPublisherMock.Object);
            ICommand command = new CreateAccountCommand{
                Name = accountName
            };

            commandHandler.Handle((CreateAccountCommand) command);

            eventPublisherMock.VerifyAll();
        }

        [Fact]
        public void TestCommandHandleAccountGuidExistsInDb()
        {
            var accountName = "testName";
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var counter = 0;
            var accountGuid = Guid.Empty;
            accountServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns((Guid guid) =>
            {
                var aggregate = new AccountAggregate();
                if (counter > 3)
                {
                    accountGuid = guid;
                    return aggregate;
                }
                counter++;
                aggregate.Apply(new CreateAccountEvent {ItemGuid = guid});
                return aggregate;
            });
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<CreateAccountEvent>())).Callback((IEvent e) =>
            {
                Assert.NotEqual(Guid.Empty, accountGuid);
                Assert.IsType<CreateAccountEvent>(e);
                CreateAccountEvent createAccountEvent = (CreateAccountEvent) e;
                Assert.Equal(accountName, createAccountEvent.AccountName);
                Assert.Equal(accountGuid, createAccountEvent.ItemGuid);
                Assert.Equal(Guid.Empty, createAccountEvent.EventGuid);
            });
            var commandHandler = new CreateAccountCommandHandler(accountServiceMock.Object, eventPublisherMock.Object);
            ICommand command = new CreateAccountCommand{
                Name = accountName
            };

            commandHandler.Handle((CreateAccountCommand) command);

            accountServiceMock.VerifyAll();
            eventPublisherMock.VerifyAll();
        }
    }
}