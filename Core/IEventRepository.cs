using System;
using System.Collections.Generic;

namespace Core
{
    public interface IEventRepository
    {
        void Save(IEvent @event);
        List<IEvent> GetByItemGuid(Guid aggregateGuid);
    }
}
