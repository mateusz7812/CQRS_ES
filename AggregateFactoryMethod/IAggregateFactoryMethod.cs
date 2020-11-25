using System;
using System.Collections.Generic;
using System.Text;
using CommandHandlers;

namespace AggregateFactoryMethod
{
    public interface IAggregateFactoryMethod
    { 
        IAggregate CreateAggregate(Type type);
    }
}
