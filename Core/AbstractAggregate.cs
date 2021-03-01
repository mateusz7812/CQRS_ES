using System;
using System.Collections.Generic;

namespace Core
{
    public abstract class AbstractAggregate : IAggregate
    {
        public Guid Guid { get; protected set; }
        public abstract void Apply(IEvent @event);

        public void From(List<IEvent> events)
        {
            foreach (IEvent @event in events)
            {
                Apply(@event);
            }
        }
    }
}