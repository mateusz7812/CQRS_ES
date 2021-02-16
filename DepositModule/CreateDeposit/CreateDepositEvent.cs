using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace DepositModule.CreateDeposit
{
    public class CreateDepositEvent: IEvent
    {
        public CreateDepositEvent(Guid eventGuid, Guid itemGuid)
        {
            EventGuid = eventGuid;
            ItemGuid = itemGuid;
        }

        public Guid EventGuid { get; }
        public Guid ItemGuid { get; }
    }
}
