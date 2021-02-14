using System;
using System.Collections.Generic;

namespace Core
{
    public class AggregateService<T> : AbstractObservable<IEvent>, IAggregateService<T> where T : IAggregate
    {
        private readonly IRepository _repository;
        private readonly IAggregateFactoryMethod<T> _aggregateFactoryMethod;

        public AggregateService(IRepository repository, IAggregateFactoryMethod<T> aggregateFactoryMethod)
        {
            _repository = repository;
            _aggregateFactoryMethod = aggregateFactoryMethod;
        }

        public T Load(Guid aggregateGuid)
        {
            T aggregator = _aggregateFactoryMethod.CreateAggregate();
            List<IEvent> events = _repository.GetByItemGuid(aggregateGuid);
            aggregator.From(events);
            return aggregator;
        }


        public void SaveAndPublish(IEvent @event)
        {
            _repository.Save(@event);
            NotifyObservers(@event);
        }
    }
}
