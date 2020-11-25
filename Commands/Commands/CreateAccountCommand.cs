using System;

namespace Commands.Commands
{
    public class CreateAccountCommand: ICommand
    {
        public Guid AccountGuid { get; }

        public CreateAccountCommand(Guid accountGuid)
        {
            AccountGuid = accountGuid;
        }
    }
}
