using System;
using System.Collections.Generic;
using System.Text;
using Events;

namespace CommandHandlers
{
    public interface IAggregate
    {
        void Apply(IEvent @event);
        void From(List<IEvent> events);
    }
}
