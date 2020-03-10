using System;
using System.Collections.Generic;
using System.Text;

namespace EventsAndCommands.Commands
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
