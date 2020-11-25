using System;
using System.Collections.Generic;
using System.Text;

namespace Bus
{
    public interface IBus<T>
    {
        void Add(T item);
        void HandleNext();
        bool IsBusEmpty { get; }
    }
}
