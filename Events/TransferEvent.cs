using System;
using Core;
using Currencies;

namespace Events
{
    public class TransferEvent: IEvent
    {
        public Guid EventGuid { get; set; }
        public Guid ItemGuid { get; init; }
        public Guid TransfersGuid { get; init; }
        public Currency Currency { get; init; }
    }
}
