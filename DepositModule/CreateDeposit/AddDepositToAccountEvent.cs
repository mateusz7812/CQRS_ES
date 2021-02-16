using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace DepositModule.CreateDeposit
{
    public class AddDepositToAccountEvent: IEvent
    {
        public AddDepositToAccountEvent(Guid eventGuid, Guid itemGuid, Guid depositId)
        {
            EventGuid = eventGuid;
            ItemGuid = itemGuid;
            DepositId = depositId;
        }

        public Guid EventGuid { get; }
        public Guid ItemGuid { get; }
        public Guid DepositId { get; }
    }
}
