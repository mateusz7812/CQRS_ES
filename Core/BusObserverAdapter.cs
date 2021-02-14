using System;

namespace Core
{
    public class BusObserverAdapter<T> : IObserver<T> where T : IHandleable
    {
        private readonly IBus<T> _bus;

        public BusObserverAdapter(IBus<T> bus)
        {
            _bus = bus;
        }

        public void OnCompleted() { }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(T handleable)
        {
            _bus.Add(handleable);
        }
    }
}
