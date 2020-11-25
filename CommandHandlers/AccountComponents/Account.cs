using System;
using System.Collections.Generic;
using Events;
using Events.Events;

namespace CommandHandlers.AccountComponents
{
    public class Account : IAggregate
    {
        public Guid Guid { get; private set; }

        public void Apply(IEvent @event)
        {
            if (@event is CreateAccountEvent accountEvent)
            {
                Apply(accountEvent);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Apply(CreateAccountEvent createAccountEvent)
        {
            Guid = createAccountEvent.AccountGuid;
        }

        public void From(List<IEvent> events)
        {
            foreach(var @event in events){
                Apply(@event);
            }
        }
    }
}
