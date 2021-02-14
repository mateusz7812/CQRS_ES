using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class HandlerFactoryMethod<T> : IHandlerFactoryMethod<T> where T : IHandleable
    {
        private readonly List<IHandler<T>> _handlers = new List<IHandler<T>>();

        public void AddHandler(IHandler<T> handler)
        {
            _handlers.Add(handler);
        }

        public IHandler<T> CreateHandler(T handleable)
        {
            return _handlers
                .FirstOrDefault(h => h.CanHandle(handleable));
        }
    }
}
