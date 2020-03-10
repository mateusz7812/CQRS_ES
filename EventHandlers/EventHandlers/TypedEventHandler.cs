using System;
using EventsAndCommands;

namespace EventHandlers.EventHandlers
{
    public abstract class TypedEventHandler<T>: IEventHandler where T: IEvent
    {
        public Type EventType => typeof(T);
        public abstract void Handle(IEvent @event);
    }
}