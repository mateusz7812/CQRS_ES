using System;
using Core;

namespace Events
{
    public class CreateDepositEvent: IEvent
    {
        public Guid EventGuid { get; init; }
        public Guid ItemGuid { get; init; }
    }
}
