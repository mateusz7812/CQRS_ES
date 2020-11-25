using System;
using System.Collections.Generic;
using Cache;
using CommandHandlers;
using EventHandlers;
using EventHandlersFactoryMethods;
using Events;

namespace EventBus
{
    public class DefaultEventBus : IEventBus
    {

        private readonly IEventHandlerFactoryMethod _factoryMethod;
        private readonly ICache<IEvent> _cache;

        public DefaultEventBus(IEventHandlerFactoryMethod factoryMethod, ICache<IEvent> cache)
        {
            _factoryMethod = factoryMethod;
            _cache = cache;
        }

        public void AddEvent(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public void HandleNext()
        {
            var next_event = _cache.First();
            _factoryMethod
        }

        public bool IsBusEmpty => true;
    }
}
