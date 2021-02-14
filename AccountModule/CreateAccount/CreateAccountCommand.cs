using System;
using Core;

namespace AccountModule.CreateAccount
{
    public class CreateAccountCommand : ICommand
    {
        public Guid AccountGuid { get; }

        public CreateAccountCommand(Guid accountGuid)
        {
            AccountGuid = accountGuid;
        }

    }
}
