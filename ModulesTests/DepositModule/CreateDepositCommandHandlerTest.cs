using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using AccountModule.Write;
using Commands;
using Core;
using DepositModule.CreateDeposit;
using DepositModule.Write;
using Events;
using Moq;
using Xunit;

namespace ModulesTests.DepositModule
{
    public class CreateDepositCommandHandlerTest
    {
        [Fact]
        public void TestCanHandle()
        {
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var accountId = Guid.Empty;
            ICommand command = new CreateDepositCommand {AccountId = accountId};

            var canHandle = commandHandler.CanHandle(command);

            Assert.True(canHandle);
        }

        [Fact]
        public void TestCanHandleFalse()
        {
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var commandMock = Mock.Of<ICommand>();

            var canHandle = commandHandler.CanHandle(commandMock);

            Assert.False(canHandle);
        }

        class MockAccountAggregate : AccountAggregate
        {
            public MockAccountAggregate(Guid guid)
            {
                Guid = guid;
            }
        }

        [Fact]
        public void TestHandle()
        {
            var accountId = Guid.NewGuid();
            var publishedEvents = new List<IEvent>();
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            accountServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns(() => new MockAccountAggregate(accountId));
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<IEvent>())).Callback((IEvent e) => publishedEvents.Add(e));
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            ICommand command = new CreateDepositCommand{AccountId = accountId};

            commandHandler.Handle(command);

            Assert.Equal(2, publishedEvents.Count);
            var first = publishedEvents[0];
            Assert.True(first is CreateDepositEvent);
            var second = publishedEvents[1];
            Assert.True(second is AddDepositToAccountEvent);
            Assert.Equal(first.ItemGuid, ((AddDepositToAccountEvent) second).DepositId);
        }

        [Fact]
        public void TestHandleEmptyGuid()
        {
            var publishedEvents = new List<IEvent>();
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<IEvent>())).Callback((IEvent e) => publishedEvents.Add(e));
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var accountId = Guid.Empty;
            ICommand command = new CreateDepositCommand{AccountId = accountId};

            commandHandler.Handle(command);

            Assert.Single(publishedEvents);
            var publishedEvent = publishedEvents.First();
            Assert.True(publishedEvent is ErrorEvent);
            Assert.Equal("Account Guid empty", ((ErrorEvent) publishedEvent).ErrorMessage);
            Assert.Equal("{\"AccountId\":\"" + Guid.Empty + "\"}", ((ErrorEvent) publishedEvent).ErrorDataJson);
        }

        [Fact]
        public void TestHandleUserNotExist()
        {
            var publishedEvents = new List<IEvent>();
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            var accountServiceMock = new Mock<IAggregateService<AccountAggregate>>();
            accountServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns(() => new AccountAggregate());
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<IEvent>())).Callback((IEvent e) => publishedEvents.Add(e));
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var accountId = Guid.NewGuid();
            ICommand command = new CreateDepositCommand{AccountId = accountId};

            commandHandler.Handle(command);

            Assert.Single(publishedEvents);
            var publishedEvent = publishedEvents.First();
            Assert.True(publishedEvent is ErrorEvent);
            Assert.Equal("Account not found", ((ErrorEvent) publishedEvent).ErrorMessage);
            Assert.Equal(JsonSerializer.Serialize(new AccountAggregate()), ((ErrorEvent) publishedEvent).ErrorDataJson);
        }
    }
}