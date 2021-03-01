using System;
using Currencies;
using Xunit;

namespace Tests
{
    public class CurrenciesTest
    {
        [InlineData(10)]
        [InlineData(10.5)]
        [InlineData(10.55)]
        [InlineData(0)]
        [Theory]
        public void CreateDollarsTest(double value)
        {
            CurrencyValue dollars = Dollars.Of(value);
            Assert.Equal(value, dollars);
        }

        [Fact]
        public void CreateTwoPointPrecisionWithThreePointPrecisionNumberTest()
        {
            Assert.Throws<ArgumentException>(() => Dollars.Of(10.501));
        }

        [Fact]
        public void CreateTwoPointPrecisionWitNegativeNumberTest()
        {
            Assert.Throws<ArgumentException>(() => Dollars.Of(-10));
        }

        [InlineData(1, 2, 3)]
        [InlineData(1.1, 2.2, 3.3)]
        [InlineData(1.11, 2.22, 3.33)]
        [Theory]
        public void AddTwoDollarsTest(double first, double second, double result)
        {
            var firstDollar = Dollars.Of(first);
            var secondDollar = Dollars.Of(second);
            var resultDollar = Dollars.Of(result);
            Assert.Equal(resultDollar, firstDollar + secondDollar);
        }

        [Fact]
        public void AddTwoOthersCurrencies()
        {
            var first = Dollars.Of(22.50);
            var second = Zloty.Of(22.50);
            Assert.Throws<ArgumentException>(() => first + second);
        }

        [InlineData(3, 2, 1)]
        [InlineData(3.3, 2.2, 1.1)]
        [InlineData(3.33, 2.22, 1.11)]
        [Theory]
        public void SubtractTwoDollarsTest(double first, double second, double result)
        {
            var firstDollar = Dollars.Of(first);
            var secondDollar = Dollars.Of(second);
            var resultDollar = Dollars.Of(result);
            Assert.Equal(resultDollar, firstDollar - secondDollar);
        }

        [Fact]
        public void SubtractTwoOthersCurrencies()
        {
            var first = Dollars.Of(22.50);
            var second = Zloty.Of(22.50);
            Assert.Throws<ArgumentException>(() => first - second);
        }
    }
}