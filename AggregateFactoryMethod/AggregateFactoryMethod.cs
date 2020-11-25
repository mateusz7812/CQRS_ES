using System;
using System.Collections.Generic;
using System.Text;
using CommandHandlers;

namespace AggregateFactoryMethod
{
    class AggregateFactoryMethod: IAggregateFactoryMethod
    {
        public IAggregate CreateAggregate(Type type)
        {
            return typeof(IAggregate).IsAssignableFrom(type) ? (IAggregate) Activator.CreateInstance(type) : null;
        }
    }
}
