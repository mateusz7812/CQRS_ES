using System;
using System.Collections.Generic;
using System.Text;
using EventHandlers;
using EventsAndCommands;

namespace EventBus
{
    public interface IEventBus
    {
        void AddEventHandler(IEventHandler commandHandler);
        void AddEvent(IEvent @event);
        void HandleNext();
        bool IsBusEmpty { get; }
    }
}
