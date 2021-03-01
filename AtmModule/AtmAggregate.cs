using System;
using Core;
using Events;

namespace AtmModule
{
    public class AtmAggregate: AbstractAggregate
    {
        public override void Apply(IEvent @event)
        {
            switch (@event)
            {
                case CreateAtmEvent createAtmEvent:
                    Guid = createAtmEvent.ItemGuid;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}