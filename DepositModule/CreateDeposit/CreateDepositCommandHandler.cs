using System;
using Commands;
using Core;
using DepositModule.Write;
using Events;

namespace DepositModule.CreateDeposit
{
    public class CreateDepositCommandHandler : AbstractCommandHandler<CreateDepositCommand>
    {
        private readonly IAggregateService<DepositAggregate> _depositAggregateService;
        private readonly IAggregateService<IAggregate> _accountAggregateService;
        private readonly IEventPublisher _eventPublisher;

        public CreateDepositCommandHandler(IAggregateService<DepositAggregate> depositAggregateService,
            IAggregateService<IAggregate> accountAggregateService,
            IEventPublisher eventPublisher): 
            base(eventPublisher)
        {
            _depositAggregateService = depositAggregateService;
            _accountAggregateService = accountAggregateService;
            _eventPublisher = eventPublisher;
        }

        public override void Handle(ICommand item)
        {
            var createDepositCommand = (CreateDepositCommand) item;
            var accountId = createDepositCommand.AccountId;
            if (accountId.Equals(Guid.Empty))
            {
                var errorEvent = new ErrorEvent(Guid.NewGuid(), Guid.Empty, createDepositCommand, "Account Guid empty");
                _eventPublisher.Publish(errorEvent);
                return;
            }

            var account = _accountAggregateService.Load(accountId);
            if (account.Guid.Equals(Guid.Empty))
            {
                var errorEvent = new ErrorEvent(Guid.NewGuid(), accountId, account, "Account not found");
                _eventPublisher.Publish(errorEvent);
                return;
            }

            var depositGuid = GetDepositGuid();
            
            var createDepositEvent = new CreateDepositEvent{
                ItemGuid = depositGuid,
                AccountGuid = accountId
            };
            _eventPublisher.Publish(createDepositEvent);
            
            var addDepositToAccountEvent = new AddDepositToAccountEvent
            {
                ItemGuid = accountId,
                DepositId = depositGuid
            };
            _eventPublisher.Publish(addDepositToAccountEvent);
        }

        private Guid GetDepositGuid()
        {
            while (true)
            {
                var guid = Guid.NewGuid();
                var aggregate = _depositAggregateService.Load(guid);
                if (aggregate.Guid == Guid.Empty) return guid;
            }
        }
    }
}