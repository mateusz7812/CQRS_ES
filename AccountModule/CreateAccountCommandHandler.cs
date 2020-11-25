using System;
using CommandHandlers.AggregatesServices;
using CommandHandlers.CommandHandlers;
using Commands;
using Commands.Commands;
using Events.Events;

namespace CommandHandlers.AccountComponents
{
    public class CreateAccountCommandHandler : TypedCommandHandler<CreateAccountEvent>
    {
        private readonly IAggregateService<Account> _accountService;

        public CreateAccountCommandHandler(IAggregateService<Account> accountService)
        {
            _accountService = accountService;
        }

        public override void Handle(ICommand command)
        {
            var createAccountCommand = (CreateAccountCommand) command;
            var accountGuid = createAccountCommand.AccountGuid;

            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            _accountService.SaveAndPublish(createAccountEvent);
            NotifyObservers(createAccountEvent);
        }

        public override bool CanHandle(ICommand command)
        {
            return command is CreateAccountCommand;
        }

        private bool CommandIsCorrect(CreateAccountCommand command) => 
            command.AccountGuid != Guid.Empty;
    }
}
