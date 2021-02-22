using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Currencies;

namespace Events
{
    public class TransferEvent: IEvent
    {
        public Guid EventGuid { get; init; }
        public Guid ItemGuid { get; init; }
        public Guid TransfersGuid { get; init; }
        public ICurrency Currency { get; init; }
    }
}
