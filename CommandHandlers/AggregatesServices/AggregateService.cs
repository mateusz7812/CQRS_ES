using System;
using System.Collections.Generic;
using EventsAndCommands;

namespace CommandHandlers.AggregatesServices
{
    public class AggregateService<T> : IAggregateService<T>, IObservable where T : IAggregate
    {
        private readonly List<IObserver> _observers = new List<IObserver>();
        private readonly IRepository _repository;
        
        public AggregateService(IRepository repository) => _repository = repository;

        public void AddObserver(IObserver observer) => _observers.Add(observer);

        public T Load(Guid aggregateGuid)
        {
            var aggregator = Activator.CreateInstance<T>();
            var events = _repository.GetEventsOfItemGuid(aggregateGuid);
            aggregator.From(events);
            return aggregator;
        }

        public void NotifyObservers(IEvent @event) => _observers.ForEach(o => o.Update(@event));

        public void SaveAndPublish(IEvent @event)
        {
            _repository.Save(@event);
            NotifyObservers(@event);
        }
    }
}
