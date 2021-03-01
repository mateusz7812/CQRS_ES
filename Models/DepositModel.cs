using System;
using Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Currencies;

namespace Models
{
    public class DepositModel: IModel
    {
        [Key]
        public int ModelDbId { get; set; }
        [Index(IsUnique = true)]
        public Guid Guid { get; set; }
        [NotNull]
        public virtual AccountModel Account { get; set; }

        public Decimal CurrencyValue { get; set; }
        public CurrenciesEnum CurrencyType { get; set; }
    }
}
