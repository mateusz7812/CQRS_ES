using System;
using System.Collections.Generic;

namespace Core
{
    public abstract class AbstractObservable<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();
        public void AddObserver(IObserver<T> observer)
        {
            _observers.Add(observer);
        }

        protected void NotifyObservers(T item)
        {
            _observers.ForEach(o => o.OnNext(item));
        }

        private class Disposable : IDisposable
        {
            private readonly Action _removeAction;

            public Disposable(Action removeAction)
            {
                _removeAction = removeAction;
            }

            public void Dispose()
            {
                _removeAction.Invoke();
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            return new Disposable(() => _observers.Remove(observer));
        }
    }
}
