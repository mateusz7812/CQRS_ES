using System;
using System.Collections.Generic;
using Cache;
using CommandHandlers;
using EventBus;
using EventHandlers;
using EventHandlers.EventHandlers;
using EventHandlersFactoryMethods;
using Events;
using Events.Events;
using Moq;
using Xunit;

namespace EventBusTests
{
    public class EventBusTests
    {
        private class Observable : IObservable<IEvent>
        {
            private readonly List<IObserver<IEvent>> _observers = new List<IObserver<IEvent>>();
            public void NotifyObservers(IEvent @event) => _observers.ForEach(o=>o.OnNext(@event));

            private class Disposable : IDisposable
            {
                private readonly Action _removeAction;

                public Disposable(Action removeAction)
                {
                    _removeAction = removeAction;
                }

                public void Dispose()
                {
                    _removeAction.Invoke();
                }
            }

            public IDisposable Subscribe(IObserver<IEvent> observer)
            {
                _observers.Add(observer);
                return new Disposable(() => _observers.Remove(observer));
            }
        }

        [Fact]
        public void TestEventHandle()
        {
            var observable = new Observable();
            var eventHandlerMock = new Mock<TypedEventHandler<CreateAccountEvent>>();
            eventHandlerMock.Setup((handler => handler.Handle(It.IsAny<CreateAccountEvent>())));

            EventHandlerFactoryMethod eventHandlerFactoryMethod = new EventHandlerFactoryMethod();
            ICache<IEvent> eventsCache = new Cache<IEvent>();
            
            var eventBus = new EventBus.DefaultEventBus(eventHandlerFactoryMethod, eventsCache);
            eventHandlerFactoryMethod.AddEventHandler(eventHandlerMock.Object);
            EventBusObserverAdapter eventBusObserverAdapter = new EventBusObserverAdapter(eventBus);
            observable.Subscribe(eventBusObserverAdapter);
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), Guid.NewGuid());
            observable.NotifyObservers(createAccountEvent);

            eventBus.HandleNext();

            eventHandlerMock.VerifyAll();
        }
    }
}
