using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using EventHandlers;
using Events;

namespace EventHandlersFactoryMethods
{
    public class EventHandlerFactoryMethod: IEventHandlerFactoryMethod
    {
        private readonly List<IEventHandler> _eventHandlers = new List<IEventHandler>();

        public IEventHandler CreateHandler(IEvent @event)
        {
            return _eventHandlers
                .FirstOrDefault(h => h.CanHandle(@event));
        }

        public void AddEventHandler(IEventHandler eventHandler)
        {
            _eventHandlers.Add(eventHandler);
        }
    }
}
