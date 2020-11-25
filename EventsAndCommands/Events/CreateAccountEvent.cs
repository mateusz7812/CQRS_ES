using System;

namespace Events.Events
{
    public class CreateAccountEvent : IEvent
    {

        public CreateAccountEvent(Guid eventGuid, Guid accountGuid)
        {
            EventGuid = eventGuid;
            ItemGuid = accountGuid;
        }

        public Guid EventGuid { get; }
        public Guid ItemGuid { get; }
    }
}
