using System;
using AccountModule.Write;
using Core;

namespace AccountModule.CreateAccount
{
    public class CreateAccountCommandHandler : IHandler<ICommand>
    {
        private readonly IAggregateService<AccountAggregate> _accountService;
        private readonly IEventPublisher _eventPublisher;

        public CreateAccountCommandHandler(IAggregateService<AccountAggregate> accountService, IEventPublisher eventPublisher)
        {
            _accountService = accountService;
            _eventPublisher = eventPublisher;
        }

        public void Handle(ICommand command)
        {
            var createAccountCommand = (CreateAccountCommand)command;
            
            var accountGuid = Guid.NewGuid();

            var eventGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(eventGuid, accountGuid);
            _eventPublisher.Publish(createAccountEvent);
        }

        public bool CanHandle(ICommand command)
        {
            return command is CreateAccountCommand;
        }
    }
}
