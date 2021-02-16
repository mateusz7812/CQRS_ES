using System;
using System.Collections.Generic;
using AccountModule.CreateAccount;
using Core;

namespace AccountModule.Write
{
    public class AccountAggregate : IAggregate
    {
        public Guid Guid { get; protected set; }
        public List<Guid> DepositsGuides { get; }

        public AccountAggregate()
        {
            DepositsGuides = new List<Guid>();
        }

        public void Apply(IEvent @event)
        {
            switch (@event)
            {
                case CreateAccountEvent accountEvent:
                    Guid = accountEvent.ItemGuid;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }


        public void From(List<IEvent> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }
    }
}