using System;
using System.Collections.Generic;
using Optionals;

namespace Core
{
    public interface IEventRepository
    {
        void Save(IEvent @event);
        List<IEvent> GetByItemGuid(Guid aggregateGuid);
        Optional<IEvent> GetByEventGuid(Guid guid);
    }
}
