using System;
using System.Collections.Generic;
using Core;
using Events;

namespace DepositModule.Write
{
    public class DepositAggregate: IAggregate
    {
        public Guid Guid { get; private set; }
        public Guid AccountGuid { get; private set; }

        public void Apply(IEvent @event)
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

        public void From(List<IEvent> events)
        {
            foreach (IEvent @event in events)
            {
                Apply(@event);
            }
        }
    }
}
