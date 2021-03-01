namespace Currencies
{
    public static class Zloty
    {
        public static Currency Of(CurrencyValue value) => new() { CurrencyType = CurrenciesEnum.PLN, CurrencyValue = value };
    }
}