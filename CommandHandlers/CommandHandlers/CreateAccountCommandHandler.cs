using System;
using CommandHandlers.Aggregates;
using EventsAndCommands;
using EventsAndCommands.Commands;
using EventsAndCommands.Events;

namespace CommandHandlers.CommandHandlers
{
    public class CreateAccountCommandHandler : TypedCommandHandler<CreateAccountCommand>
    {
        private readonly IAggregateService<Account> _accountService;

        public CreateAccountCommandHandler() { }

        public CreateAccountCommandHandler(IAggregateService<Account> accountService)
        {
            _accountService = accountService;
        }

        public override void Handle(ICommand command)
        {
            if (!(command is CreateAccountCommand accountCommand)) return;
            var accountGuid = accountCommand.AccountGuid;
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            _accountService.SaveAndPublish(createAccountEvent);
        }

        public override bool CommandIsCorrect(ICommand command) 
            => ((CreateAccountCommand) command).AccountGuid != Guid.Empty;
    }
}
