using System;
using Core;
using Currencies;

namespace Commands
{
    public class TransferOnDepositFromDepositCommand : ICommand
    {
        public Guid SourceDepositId { get; init; }
        public Guid DestinationDepositId { get; init; }
        public Currency Currency { get; set; }
    }
}