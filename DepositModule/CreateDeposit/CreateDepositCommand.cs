using System;
using Core;

namespace DepositModule.CreateDeposit
{
    public class CreateDepositCommand: ICommand
    {
        public Guid AccountId { get; }

        public CreateDepositCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}