using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace DepositModule.CreateDeposit
{
    class AddDepositToAccountEvent: IEvent
    {
        public AddDepositToAccountEvent(Guid eventGuid, Guid itemGuid)
        {
            EventGuid = eventGuid;
            ItemGuid = itemGuid;
        }

        public Guid EventGuid { get; }
        public Guid ItemGuid { get; }
    }
}
