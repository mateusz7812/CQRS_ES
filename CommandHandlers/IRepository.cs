using System;
using System.Collections.Generic;
using System.Text;
using Events;

namespace CommandHandlers
{
    public interface IRepository
    {
        void Save(IEvent @event);
        List<IEvent> GetByItemGuid(Guid aggregateGuid);
    }
}
