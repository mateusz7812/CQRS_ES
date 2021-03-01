using System;
using Core;
using Currencies;

namespace Commands
{
    public class TransferOnDepositByAtmCommand: ICommand
    {
        public Guid AtmId { get; init; }
        public Guid DepositId { get; init; }
        public Currency Currency { get; init; }
    }
}