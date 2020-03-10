using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace CommandHandlers
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);
        void NotifyObservers(IEvent @event);
    }
}
