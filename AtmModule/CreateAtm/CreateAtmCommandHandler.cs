
using System;
using Commands;
using Core;
using Events;

namespace AtmModule.CreateAtm
{
    public class CreateAtmCommandHandler: AbstractCommandHandler<CreateAtmCommand>
    {
        private readonly IAggregateService<AtmAggregate> _aggregateService;

        public CreateAtmCommandHandler(IAggregateService<AtmAggregate> aggregateService, IEventPublisher eventPublisher) : base(eventPublisher)
        {
            _aggregateService = aggregateService;
        }

        public override void Handle(ICommand item)
        {
            var guid = GetGuid();
            var createAtmEvent = new CreateAtmEvent{ItemGuid = guid};
            _eventPublisher.Publish(createAtmEvent);
        }

        private Guid GetGuid()
        {
            while (true)
            {
                var guid = Guid.NewGuid();
                var aggregate = _aggregateService.Load(guid);
                if (aggregate.Guid == Guid.Empty) return guid;
            }
        }
    }
}
