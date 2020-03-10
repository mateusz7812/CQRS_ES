using System;

namespace EventsAndCommands
{
    public interface IEvent
    {
        Guid EventGuid { get; }
        Guid ItemGuid { get; }
    }
}
