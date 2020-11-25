using System;
using System.Collections.Generic;
using System.Text;
using Cache;

namespace Bus
{
    public class Bus<T>: IBus<T> where T:IHandleable
    {

        private readonly IHandlerFactoryMethod<T> _factoryMethod;
        private readonly ICache<T> _cache;

        public Bus(IHandlerFactoryMethod<T> factoryMethod, ICache<T> cache)
        {
            _factoryMethod = factoryMethod;
            _cache = cache;
        }
        public void Add(T item)
        {
            _cache.Add(item);
        }

        public void HandleNext()
        {
            var command = _cache.First();
            var handler = _factoryMethod.CreateHandler(command);

            if (handler == null)
                throw new NotSupportedException("Handler not found");

            handler.Handle(command);
            _cache.Remove(command);
        }

        public bool IsBusEmpty => _cache.IsEmpty;
    }


}
