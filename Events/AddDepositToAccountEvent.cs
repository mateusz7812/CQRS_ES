using System;
using Core;

namespace Events
{
    public class AddDepositToAccountEvent: IEvent
    {
        public Guid EventGuid { get; init; }
        public Guid ItemGuid { get; init; }
        public Guid DepositId { get; init; }
    }
}