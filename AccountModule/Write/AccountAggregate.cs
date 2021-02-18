using System;
using System.Collections.Generic;
using Core;
using Events;

namespace AccountModule.Write
{
    public class AccountAggregate : IAggregate
    {
        public Guid Guid { get; protected set; }
        public string Name { get; protected set; }
        public List<Guid> DepositsGuides { get; }

        public AccountAggregate()
        {
            DepositsGuides = new List<Guid>();
        }

        public void Apply(IEvent @event)
        {
            switch (@event)
            {
                case CreateAccountEvent createAccountEvent:
                    Guid = createAccountEvent.ItemGuid;
                    Name = createAccountEvent.AccountName;
                    break;
                case AddDepositToAccountEvent addDepositToAccountEvent:
                    DepositsGuides.Add(addDepositToAccountEvent.DepositId);
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