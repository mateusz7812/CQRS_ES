using System;
using Core;

namespace AccountModule.CreateAccount
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
