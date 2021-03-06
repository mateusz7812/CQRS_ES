﻿using System;
using Core;
using Currencies;

namespace Commands
{

    public class TransferCommand: ICommand
    {
        public Guid SourceDepositId { get; init; }
        public Guid DestinationDepositId { get; init; }
        public Currency Currency { get; init; }
    }
}