using System;
using System.Collections.Generic;
using Cache;
using CommandHandlerFactoryMethods;
using CommandHandlers;
using Commands;

namespace CommandBus
{
    public class DefaultCommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactoryMethod _factoryMethod;
        private readonly ICache<ICommand> _cache;

        public DefaultCommandBus(ICommandHandlerFactoryMethod factoryMethod, ICache<ICommand> cache)
        {
            _factoryMethod = factoryMethod;
            _cache = cache;
        }

        public void Add(ICommand command)
        {
            _cache.Add(command);
        }

        public void HandleNext()
        {
            var command = _cache.First();
            var handler = _factoryMethod.CreateHandler(command);
            
            if(handler == null) 
                throw new NotSupportedException("Handler not found");
            
            handler.Handle(command);
            _cache.Remove(command);
        }

        public bool IsBusEmpty => _cache.IsEmpty;
    }
}
