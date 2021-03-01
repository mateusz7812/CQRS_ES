using System;
using System.Collections.Generic;

namespace Core
{
    public interface IAggregate
    {
        Guid Guid{ get; } 
        void Apply(IEvent @event);

        void From(List<IEvent> events);
    }
}
