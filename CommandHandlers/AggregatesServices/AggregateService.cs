using System;

namespace Core
{
    public class AggregateService<T>: AbstractObservable<IEvent>, IAggregateService<T> where T : IAggregate
    {
        private readonly IRepository _repository;
        private readonly IAggregateFactoryMethod _aggregateFactoryMethod;
        
        public AggregateService(IRepository repository, IAggregateFactoryMethod aggregateFactoryMethod)
        {
            _repository = repository;
            _aggregateFactoryMethod = aggregateFactoryMethod;
        }


        public T Load(Guid aggregateGuid)
        {
            var aggregator = _aggregateFactoryMethod.CreateAggregate<T>();
            var events = _repository.GetByItemGuid(aggregateGuid);
            aggregator.From(events);
            return (T) aggregator;
        }


        public void SaveAndPublish(IEvent @event)
        {
            _repository.Save(@event);
            NotifyObservers(@event);
        }
    }
}
