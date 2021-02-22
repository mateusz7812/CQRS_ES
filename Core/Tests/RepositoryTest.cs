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
            var accountGuid = Guid.NewGuid();
            var eventGuid = Guid.NewGuid();
            var @event = Mock.Of<IEvent>(e => e.ItemGuid == accountGuid && e.EventGuid == eventGuid);
            var eventStoreMock = new Mock<IEventStore>();
            var eventsDict = new Dictionary<Guid, List<IEvent>>();
            eventStoreMock.Setup(m => m.Save(It.IsAny<IEvent>())).Callback((IEvent e) => eventsDict[e.ItemGuid] = new List<IEvent>{e});
            eventStoreMock.Setup(m => m.FindByItemGuid(It.IsAny<Guid>())).Returns((Guid guid) => eventsDict[guid]);
            var repository = new EventRepository(eventStoreMock.Object);

            repository.Save(@event);
            var events = repository.GetByItemGuid(accountGuid);

            Assert.Single(events);
            Assert.True(events[0] == @event);
        }
    }
}
