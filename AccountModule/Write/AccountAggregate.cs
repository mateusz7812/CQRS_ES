using System;
using System.Collections.Generic;
using Core;
using Events;

namespace AccountModule.Write
{
    public class AccountAggregate : AbstractAggregate
    {
        public string Name { get; protected set; }
        public List<Guid> DepositsGuides { get; }

        public AccountAggregate()
        {
            DepositsGuides = new List<Guid>();
        }

        public override void Apply(IEvent @event)
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
    }
}