using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace Core.Tests
{
    public class EventPublisherTest
    {
        [Fact]
        public void TestObservableEventPublisher()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(m => m.Save(It.IsAny<IEvent>()));

            var observerMock = new Mock<IObserver<IEvent>>();
            observerMock.Setup(m => m.OnNext(It.IsAny<IEvent>()));

            var observableEventPublisher = new ObservableEventPublisher(eventRepositoryMock.Object);
            observableEventPublisher.AddObserver(observerMock.Object);

            var eventMock = new Mock<IEvent>();

            observableEventPublisher.Publish(eventMock.Object);

            eventRepositoryMock.VerifyAll();
            observerMock.VerifyAll();
        }
    }
}
