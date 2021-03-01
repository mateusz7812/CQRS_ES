using System;

namespace Currencies
{
    public class Currency: object
    {
        internal Currency() { }

        public CurrenciesEnum CurrencyType { get; init; }

        public CurrencyValue CurrencyValue { get; init; }

        public static implicit operator CurrenciesEnum(Currency currency)
        {
            return currency.CurrencyType;
        }

        public static implicit operator CurrencyValue(Currency currency)
        {
            return currency.CurrencyValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is Currency currency)
            {
                return currency.Equals(this);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CurrencyValue, (int) CurrencyType);
        }

        protected bool Equals(Currency currency)
        {
            return currency.CurrencyType == CurrencyType &&
                   currency.CurrencyValue.Equals(CurrencyValue);
        }

        public static Currency operator +(Currency first, Currency second)
        {
            if (first.CurrencyType != second.CurrencyType)
                throw new ArgumentException("you can not add different currencies");
            return new Currency {CurrencyType = first.CurrencyType, CurrencyValue = first.CurrencyValue + second.CurrencyValue};
        }
        public static Currency operator -(Currency first, Currency second)
        {
            if (first.CurrencyType != second.CurrencyType)
                throw new ArgumentException("you can not add different currencies");
            return new Currency {CurrencyType = first.CurrencyType, CurrencyValue = first.CurrencyValue - second.CurrencyValue};
        }
    }
}