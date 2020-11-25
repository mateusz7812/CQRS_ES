using System;
using System.Runtime.InteropServices.ComTypes;
using CommandHandlers.AccountComponents;

namespace CommandHandlers.AggregateFactoryMethod
{
    public class AggregateFactoryMethod : IAggregateFactoryMethod
    {
        public IAggregate CreateAggregate<T>()
        {
            if(typeof(T) == typeof(Account)) return new Account();
            return null;
        }
    }
}
