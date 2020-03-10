using System;
using System.Collections.Generic;
using CommandHandlers;
using EventHandlers;
using EventHandlers.EventHandlers;
using EventsAndCommands;
using EventsAndCommands.Events;
using Moq;
using Xunit;

namespace EventBusTests
{
    public class EventBusTests
    {
        private class Observable : IObservable
        {
            private readonly List<IObserver> _observers = new List<IObserver>();
            public void AddObserver(IObserver observer) => _observers.Add(observer);
            public void NotifyObservers(IEvent @event) => _observers.ForEach(o=>o.Update(@event));
        }
        

        [Fact]
        public void Test1()
        {
            var observable = new Observable();
            var eventHandlerMock = new Mock<TypedEventHandler<CreateAccountEvent>>();
            eventHandlerMock.Setup((handler => handler.Handle(It.IsAny<CreateAccountEvent>())));
            var eventBus = new EventBus.DefaultEventBus();
            eventBus.AddEventHandler(eventHandlerMock.Object);
            observable.AddObserver(eventBus);
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), Guid.NewGuid());
            observable.NotifyObservers(createAccountEvent);

            eventBus.HandleNext();

            eventHandlerMock.VerifyAll();
        }
    }
}
