using System;
using System.Collections.Generic;
using AccountModule.CreateAccount;
using Core;
using DepositModule.Write;

namespace AccountModule.Write
{
    public class AccountAggregate : IAggregate
    {
        public Guid Guid { get; private set; }
        public List<DepositAggregate> Deposits { get; }

        public AccountAggregate()
        {
            Deposits = new List<DepositAggregate>();
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