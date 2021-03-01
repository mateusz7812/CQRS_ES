using System;
using Moq;
using Optionals;
using Xunit;

namespace Core.Tests
{
    class TestEvent : IEvent
    {
        public Guid EventGuid { get; set; }
        public Guid ItemGuid { get; set; }
    }

    public class EventPublisherTest
    {
        [Fact]
        public void TestObservableEventPublisher()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(m => m.Save(It.IsAny<IEvent>()));
            eventRepositoryMock.Setup(m => m.GetByEventGuid(It.IsAny<Guid>())).Returns((Guid guid) => Codes.NotFound);
            
            var observerMock = new Mock<IObserver<IEvent>>();
            observerMock.Setup(m => m.OnNext(It.IsAny<IEvent>()));

            var observableEventPublisher = new ObservableEventPublisher(eventRepositoryMock.Object);
            observableEventPublisher.AddObserver(observerMock.Object);

            var eventMock = new TestEvent {ItemGuid = Guid.NewGuid(), EventGuid = Guid.Empty};

            observableEventPublisher.Publish(eventMock);

            eventRepositoryMock.VerifyAll();
            observerMock.VerifyAll();
        }


        [Fact]
        public void TestEventGuidExist()
        {
            var counter = 0;
            var eventGuid = Guid.Empty;
            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(m => m.Save(It.IsAny<IEvent>()));
            eventRepositoryMock.Setup(m => m.GetByEventGuid(It.IsAny<Guid>())).Returns((Guid guid) =>
            {
                if (counter > 3)
                {
                    eventGuid = guid;
                    return Codes.NotFound;
                }
                counter++;
                return new TestEvent {EventGuid = guid};
            });

            var observerMock = new Mock<IObserver<IEvent>>();
            observerMock.Setup(m => m.OnNext(It.IsAny<IEvent>()));

            var observableEventPublisher = new ObservableEventPublisher(eventRepositoryMock.Object);
            observableEventPublisher.AddObserver(observerMock.Object);

            var eventMock = new TestEvent { ItemGuid = Guid.NewGuid(), EventGuid = Guid.Empty };

            observableEventPublisher.Publish(eventMock);

            eventRepositoryMock.VerifyAll();
            observerMock.VerifyAll();
            Assert.NotEqual(Guid.Empty, eventGuid);
        }


        [Fact]
        public void TestExceptionIfEventGuidIsSet()
        {
            var itemGuid = Guid.NewGuid();

            var eventMock = new TestEvent {EventGuid = Guid.NewGuid(), ItemGuid = itemGuid};

            var observerMock = new Mock<IObserver<IEvent>>();
            observerMock.Setup(m => m.OnNext(It.Is<ErrorEvent>(e =>
                e.ItemGuid == itemGuid && e.ErrorMessage == "Event guid is generating by publisher" &&
                e.ErrorDataJson != "")));

            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(m => m.GetByEventGuid(It.IsAny<Guid>())).Returns((Guid guid) => Codes.NotFound);

            var observableEventPublisher = new ObservableEventPublisher(eventRepositoryMock.Object);
            observableEventPublisher.AddObserver(observerMock.Object);

            observableEventPublisher.Publish(eventMock);

            observerMock.VerifyAll();
        }

        [Fact]
        public void TestExceptionIfItemGuidIsEmpty()
        {
            var observerMock = new Mock<IObserver<IEvent>>();
            observerMock.Setup(m => m.OnNext(It.Is<ErrorEvent>(e =>
                e.ItemGuid == Guid.Empty && e.ErrorMessage == "Item guid is not set" && e.ErrorDataJson != "")));

            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(m => m.GetByEventGuid(It.IsAny<Guid>())).Returns((Guid guid) => Codes.NotFound);

            var observableEventPublisher = new ObservableEventPublisher(eventRepositoryMock.Object);
            observableEventPublisher.AddObserver(observerMock.Object);

            var eventMock = new TestEvent {EventGuid = Guid.Empty, ItemGuid = Guid.Empty};

            observableEventPublisher.Publish(eventMock);
        }
    }
}