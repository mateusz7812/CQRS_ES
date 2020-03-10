using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace CommandHandlers
{
    public interface IObserver
    {
        void Update(IEvent @event);
    }
}
