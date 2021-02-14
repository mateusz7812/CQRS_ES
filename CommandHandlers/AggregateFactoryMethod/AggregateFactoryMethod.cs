using System;
using System.Runtime.InteropServices.ComTypes;
using CommandHandlers.AccountComponents;

namespace Core
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
