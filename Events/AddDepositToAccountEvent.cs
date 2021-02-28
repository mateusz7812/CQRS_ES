using System;
using Core;

namespace Events
{
    public class AddDepositToAccountEvent: IEvent
    {
        public Guid EventGuid { get; set; }
        public Guid ItemGuid { get; init; }
        public Guid DepositId { get; init; }
    }
}