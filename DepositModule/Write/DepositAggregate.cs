using System;
using System.Collections.Generic;
using Core;

namespace DepositModule.Write
{
    public class DepositAggregate: IAggregate
    {

        public Guid Guid { get; private set; }
        public DepositAggregate(Guid guid)
        {
            Guid = guid;
        }

        public void Apply(IEvent @event)
        {
            throw new NotImplementedException();
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
