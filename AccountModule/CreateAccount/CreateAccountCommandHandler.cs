using System;
using AccountModule.Write;
using Core;

namespace AccountModule.CreateAccount
{
    public class CreateAccountCommandHandler : TypedCommandHandler<CreateAccountEvent>
    {
        private readonly IAggregateService<AccountAggregate> _accountService;

        public CreateAccountCommandHandler(IAggregateService<AccountAggregate> accountService)
        {
            _accountService = accountService;
        }

        public override void Handle(ICommand command)
        {
            var createAccountCommand = (CreateAccountCommand)command;
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
