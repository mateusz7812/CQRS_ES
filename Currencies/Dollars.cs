namespace Currencies
{
    public static class Dollars
    {
        public static Currency Of(CurrencyValue value) => new() { CurrencyType = CurrenciesEnum.USD, CurrencyValue = value };
    }
}