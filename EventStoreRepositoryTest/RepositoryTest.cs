
using CommandHandlers;
using EventsAndCommands.Commands;
using EventsAndCommands.Events;
using EventStore;
using Moq;
using System;
using Xunit;

namespace EventsStoreRepositoryTest
{
    public class RepositoryTest
    {
        [Fact]
        public void Test1()
        {
            var eventStoreMock = new Mock<DefaultEventStore>();
            var repository = new Repository(eventStoreMock.Object);
            var accountGuid = Guid.NewGuid();
            var @event = new CreateAccountEvent(Guid.NewGuid(), accountGuid);

            repository.Save(@event);
            var events = repository.GetEventsOfItemGuid(accountGuid);

            Assert.Single(events);
            Assert.True(events[0].GetType() == typeof(CreateAccountEvent));
            Assert.Equal(accountGuid, ((CreateAccountEvent) events[0]).AccountGuid);
        }
    }
}
