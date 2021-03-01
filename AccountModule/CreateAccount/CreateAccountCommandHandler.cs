using System;
using Commands;
using Core;
using Events;

namespace AccountModule.CreateAccount
{
    public class CreateAccountCommandHandler : AbstractCommandHandler<CreateAccountCommand>
    {
        private readonly IAggregateService<IAggregate> _accountService;

        public CreateAccountCommandHandler(IAggregateService<IAggregate> accountService,
            IEventPublisher eventPublisher) :
            base(eventPublisher)
        {
            _accountService = accountService;
        }

        public override void Handle(ICommand command)
        {
            var createAccountCommand = (CreateAccountCommand) command;
            var accountGuid = GetAccountGuid();
            var accountName = createAccountCommand.Name;
            var createAccountEvent = new CreateAccountEvent
            {
                ItemGuid = accountGuid,
                AccountName = accountName
            };
            _eventPublisher.Publish(createAccountEvent);
        }

        private Guid GetAccountGuid()
        {
            while (true)
            {
                var guid = Guid.NewGuid();
                var aggregate = _accountService.Load(guid);
                if (aggregate.Guid == Guid.Empty)
                    return guid;
            }
        }
    }
}