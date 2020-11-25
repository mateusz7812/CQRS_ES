using System;
using System.Collections.Generic;
using System.Text;
using EventHandlers;
using Events;

namespace EventBus
{
    public interface IEventBus
    {
        void AddEvent(IEvent @event);
        void HandleNext();
        bool IsBusEmpty { get; }
    }
}
