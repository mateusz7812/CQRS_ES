using System;
using Core;
using Currencies;

namespace Commands
{

    public class TransferCommand: ICommand
    {
        public interface ISideOfTransaction { }
        public class Deposit: ISideOfTransaction
        {
            public Guid DepositGuid { get; init; }
        }

        public class Atm : ISideOfTransaction
        {
            public Guid AtmGuid { get; init; }
        }

        public ISideOfTransaction SourceOfTransaction { get; init; }
        public ISideOfTransaction DestinationOfTransaction { get; init; }
        public ICurrency Currency { get; init; }
    }
}