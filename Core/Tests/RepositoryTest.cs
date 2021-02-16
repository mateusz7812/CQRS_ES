using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Core.Tests
{
    public class RepositoryTest
    {
        [Fact]
        public void Test1()
        {
            var eventStoreMock = new Mock<IEventStore>();
            var repository = new EventRepository(eventStoreMock.Object);
            var accountGuid = Guid.NewGuid();
            var @event = new Mock<IEvent>().Object;
            eventStoreMock.Setup(m => m.FindByItemGuid(It.IsAny<Guid>())).Returns(new List<IEvent> { @event });

            repository.Save(@event);
            var events = repository.GetByItemGuid(accountGuid);

            Assert.Single(events);
            Assert.True(events[0] == @event);
        }
    }
}
