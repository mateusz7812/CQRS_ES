﻿using System;
using AccountModule.Write;
using Commands;
using Core;
using DepositModule.Write;
using Events;


namespace DepositModule.CreateDeposit
{
    public class CreateDepositCommandHandler : IHandler<ICommand>
    {
        private readonly IAggregateService<DepositAggregate> _depositAggregateService;
        private readonly IAggregateService<AccountAggregate> _accountAggregateService;
        private readonly IEventPublisher _eventPublisher;

        public CreateDepositCommandHandler(IAggregateService<DepositAggregate> depositAggregateService,
            IAggregateService<AccountAggregate> accountAggregateService,
            IEventPublisher eventPublisher)
        {
            _depositAggregateService = depositAggregateService;
            _accountAggregateService = accountAggregateService;
            _eventPublisher = eventPublisher;
        }

        public void Handle(ICommand item)
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

            var depositGuid = Guid.NewGuid();

            var createDepositEventGuid = Guid.NewGuid();
            var createDepositEvent = new CreateDepositEvent{
                EventGuid = createDepositEventGuid, 
                ItemGuid = depositGuid

            };
            _eventPublisher.Publish(createDepositEvent);

            var addDepositToAccountEventGuid = Guid.NewGuid();
            var addDepositToAccountEvent = new AddDepositToAccountEvent
            {
                EventGuid = addDepositToAccountEventGuid,
                ItemGuid = accountId,
                DepositId = depositGuid
            };
            _eventPublisher.Publish(addDepositToAccountEvent);
        }

        public bool CanHandle(ICommand item)
        {
            return item is CreateDepositCommand;
        }
    }
}