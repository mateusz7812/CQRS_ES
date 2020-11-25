using System;
using System.Collections.Generic;
using System.Text;

namespace Bus
{
    public interface IHandler<T> where T: IHandleable
    {
        void Handle(T item);
        bool CanHandle(T item);
    }
}
