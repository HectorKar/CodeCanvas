using System;
using System.Threading.Tasks;

namespace CodeCanvas.ExchangeRateStrategies
{
	interface IExchangeRateStrategy
	{
		Task<decimal> Convert(decimal amount, string amountCurrencyCode, string currencyCodeToConvert, DateTime date);
	}
}
