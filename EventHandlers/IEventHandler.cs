using System;
using System.Collections.Generic;
using System.Text;
using EventsAndCommands;

namespace EventHandlers
{
    public interface IEventHandler
    {
        Type EventType { get; }
        void Handle(IEvent @event);
    }
}
