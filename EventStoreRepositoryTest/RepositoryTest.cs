using System;
using Events.Events;
using EventStore;
using Moq;
using Xunit;

namespace EventStoreRepositoryTest
{
    public class RepositoryTest
    {
        [Fact]
        public void Test1()
        {
            var eventStoreMock = new Mock<DefaultEventStore>();
            var repository = new Repository(eventStoreMock.Object);
            var accountGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);

            repository.Save(createAccountEvent);
            var events = repository.GetByItemGuid(accountGuid);

            Assert.Single(events);
            Assert.True(events[0].GetType() == typeof(CreateAccountEvent));
            Assert.Equal(accountGuid, ((CreateAccountEvent) events[0]).AccountGuid);
        }
    }
}
