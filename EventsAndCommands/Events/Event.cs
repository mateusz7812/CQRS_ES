using System;

namespace EventsAndCommands.Events
{
    public class Event : IEvent
    {
        public Guid EventGuid { get; }
        public Guid ItemGuid { get; }

        public Event(Guid eventGuid, Guid itemGuid)
        {
            EventGuid = eventGuid;
            ItemGuid = itemGuid;
        }

    }
}
