using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
            var accountServiceMock = new Mock<IAggregateService<IAggregate>>();
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
            var accountServiceMock = new Mock<IAggregateService<IAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var commandMock = Mock.Of<ICommand>();

            var canHandle = commandHandler.CanHandle(commandMock);

            Assert.False(canHandle);
        }

        class MockAggregate : AbstractAggregate
        {
            public MockAggregate(Guid guid)
            {
                Guid = guid;
            }

            public override void Apply(IEvent @event)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void TestHandle()
        {
            var accountId = Guid.NewGuid();
            var publishedEvents = new List<IEvent>();
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            depositServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns((Guid guid) => new DepositAggregate());
               
            var accountServiceMock = new Mock<IAggregateService<IAggregate>>();
            accountServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns(() => new MockAggregate(accountId));
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<IEvent>())).Callback((IEvent e) => publishedEvents.Add(e));
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var command = new CreateDepositCommand {AccountId = accountId};

            commandHandler.Handle(command);

            Assert.Equal(2, publishedEvents.Count);

            var first = publishedEvents[0];
            Assert.True(first is CreateDepositEvent);
            var createDepositEvent = (CreateDepositEvent) first;

            var second = publishedEvents[1];
            Assert.True(second is AddDepositToAccountEvent);
            var addDepositToAccountEvent = (AddDepositToAccountEvent) second;

            Assert.Equal(createDepositEvent.ItemGuid, addDepositToAccountEvent.DepositId);
            Assert.Equal(createDepositEvent.AccountGuid, addDepositToAccountEvent.ItemGuid);
        }

        [Fact]
        public void TestHandleEmptyGuid()
        {
            var publishedEvents = new List<IEvent>();
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            var accountServiceMock = new Mock<IAggregateService<IAggregate>>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<IEvent>())).Callback((IEvent e) => publishedEvents.Add(e));
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var accountId = Guid.Empty;
            ICommand command = new CreateDepositCommand {AccountId = accountId};

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
            var accountServiceMock = new Mock<IAggregateService<IAggregate>>();
            accountServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns(() => new AccountAggregate());
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<IEvent>())).Callback((IEvent e) => publishedEvents.Add(e));
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var accountId = Guid.NewGuid();
            ICommand command = new CreateDepositCommand {AccountId = accountId};

            commandHandler.Handle(command);

            Assert.Single(publishedEvents);
            var publishedEvent = publishedEvents.First();
            Assert.True(publishedEvent is ErrorEvent);
            Assert.Equal("Account not found", ((ErrorEvent) publishedEvent).ErrorMessage);
            Assert.Equal(JsonSerializer.Serialize(new AccountAggregate()), ((ErrorEvent) publishedEvent).ErrorDataJson);
        }

        [Fact]
        public void TestDepositGuidIsTaken()
        {
            var accountId = Guid.NewGuid();
            var publishedEvents = new List<IEvent>();
            var depositServiceMock = new Mock<IAggregateService<DepositAggregate>>();
            var counter = 0;
            var depositGuid = Guid.Empty;
            depositServiceMock.Setup(m=>m.Load(It.IsAny<Guid>())).Returns((Guid guid) =>
            {
                var aggregate = new DepositAggregate();
                if (counter > 3)
                {
                    depositGuid = guid;
                    return aggregate;
                }
                counter++;
                aggregate.Apply(new CreateDepositEvent { ItemGuid = guid });
                return aggregate;
            });
            var accountServiceMock = new Mock<IAggregateService<IAggregate>>();
            accountServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns(() => new MockAggregate(accountId));
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(It.IsAny<IEvent>())).Callback((IEvent e) => publishedEvents.Add(e));
            var commandHandler = new CreateDepositCommandHandler(depositServiceMock.Object, accountServiceMock.Object,
                eventPublisherMock.Object);
            var command = new CreateDepositCommand {AccountId = accountId};

            commandHandler.Handle(command);

            Assert.Equal(2, publishedEvents.Count);

            var first = publishedEvents[0];
            Assert.True(first is CreateDepositEvent);
            var createDepositEvent = (CreateDepositEvent) first;

            Assert.NotEqual(Guid.Empty, depositGuid);
            Assert.Equal(depositGuid, createDepositEvent.ItemGuid);
            Assert.Equal(Guid.Empty, publishedEvents[0].EventGuid);
            Assert.Equal(Guid.Empty, publishedEvents[1].EventGuid);
        }
    }
}