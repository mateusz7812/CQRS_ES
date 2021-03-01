using System;
using Core;
using Events;

namespace DepositModule.Write
{
    public class DepositAggregate: AbstractAggregate
    {
        public Guid AccountGuid { get; private set; }

        public override void Apply(IEvent @event)
        {
            switch (@event)
            {
                case CreateDepositEvent createDepositEvent:
                    Guid = createDepositEvent.ItemGuid;
                    AccountGuid = createDepositEvent.AccountGuid;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
