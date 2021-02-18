using System;
using Core;

namespace Commands
{
    public class CreateDepositCommand: ICommand
    {
        public Guid AccountId { get; init; }
    }
}