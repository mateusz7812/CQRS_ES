using System;
using System.Collections.Generic;
using System.Text;

namespace Bus
{
    public interface IHandlerFactoryMethod<T> where T : IHandleable
    {
        IHandler<T> CreateHandler(T handleable);
    }
}
