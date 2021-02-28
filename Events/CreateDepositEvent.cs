using System;
using Core;

namespace Events
{
    public class CreateDepositEvent: IEvent
    {
        public Guid EventGuid { get; set; }
        public Guid ItemGuid { get; init; }
        public Guid AccountGuid { get; init; }
    }
}
