using System;

namespace Currencies
{
    public class CurrencyValue : object
    {
        private readonly decimal _value;

        public decimal Value
        {
            get => _value;
            init
            {
                if (value < 0)
                    throw new ArgumentException("value can not be negative");
                if (decimal.Round(value, 2) != value)
                    throw new ArgumentException("value can be only two point precision number");
                _value = value;
            }
        }

        public static implicit operator CurrencyValue(decimal value)
            => new() {Value = value};

        public static implicit operator CurrencyValue(double value)
            => new() {Value = new decimal(value)};

        public static implicit operator CurrencyValue(int value)
            => new() {Value = new decimal(value)};

        public override bool Equals(object o)
            => o is CurrencyValue other
               && other.Equals(this);

        protected bool Equals(CurrencyValue other)
            => _value == other._value;

        public override int GetHashCode()
            => _value.GetHashCode();

        public static CurrencyValue operator +(CurrencyValue first, CurrencyValue second)
            => new() {Value = first.Value + second.Value};

        public static CurrencyValue operator -(CurrencyValue first, CurrencyValue second)
            => new() {Value = first.Value - second.Value};
    }
}