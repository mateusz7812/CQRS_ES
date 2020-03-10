using System;
using System.Collections.Generic;
using CommandHandlers;
using EventHandlers;
using EventsAndCommands;

namespace EventBus
{
    public class DefaultEventBus : IEventBus, IObserver
    {
        private readonly Queue<IEvent> _events = new Queue<IEvent>();
        private readonly Dictionary<Type, IEventHandler> _eventHandlersByEventType = new Dictionary<Type, IEventHandler>();

        public void Update(IEvent @event) => AddEvent(@event);

        public void AddEventHandler(IEventHandler commandHandler) => _eventHandlersByEventType.Add(commandHandler.EventType, commandHandler);

        public void AddEvent(IEvent @event) => _events.Enqueue(@event);

        public void HandleNext()
        {
            var @event = _events.Dequeue();
            _eventHandlersByEventType[@event.GetType()].Handle(@event);
        }

        public bool IsBusEmpty => true;
    }
}
