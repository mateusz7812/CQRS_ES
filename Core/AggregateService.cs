using System;
using System.Collections.Generic;

namespace Core
{
    public class AggregateService<T>: IAggregateService<T> where T : IAggregate
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAggregateFactoryMethod<T> _aggregateFactoryMethod;

        public AggregateService(IEventRepository eventRepository, IAggregateFactoryMethod<T> aggregateFactoryMethod)
        {
            _eventRepository = eventRepository;
            _aggregateFactoryMethod = aggregateFactoryMethod;
        }

        public T Load(Guid aggregateGuid)
        {
            T aggregator = _aggregateFactoryMethod.CreateAggregate();
            List<IEvent> events = _eventRepository.GetByItemGuid(aggregateGuid);
            aggregator.From(events);
            return aggregator;
        }


    }
}
