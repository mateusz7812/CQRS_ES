using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cache
{
    public class Cache<T>: ICache<T>
    {
        private readonly List<T> _list;

        public Cache()
        {
            _list = new List<T>();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public T First()
        {
            return _list.First();
        }

        public void Remove(T item)
        {
            _list.Remove(item);
        }

        public bool IsEmpty => !_list.Any();
    }
}
