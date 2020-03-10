using System;
using System.Collections.Generic;
using System.Text;

namespace EventsAndCommands.Events
{
    public class CreateAccountEvent: Event
    {
        public Guid AccountGuid { get; }

        public CreateAccountEvent(Guid eventGuid, Guid accountGuid) 
            : base(eventGuid, accountGuid) 
            => AccountGuid = accountGuid;
    }
}
