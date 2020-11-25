using System;
using System.Collections.Generic;
using System.Text;

namespace Cache
{
    public interface ICache<T>
    {
        void Add(T item);
        T First();
        void Remove(T item);
        bool IsEmpty { get; }
    }
}
