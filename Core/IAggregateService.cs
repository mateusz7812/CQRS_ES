using System;

namespace Core
{
    public interface IAggregateService<out T> where T : IAggregate
    {
        T Load(Guid aggregateGuid);
    }
}
