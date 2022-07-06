using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCanvas.Entities;
using CodeCanvas.Services;

namespace CodeCanvas.ExchangeRateStrategies
{
	abstract class ExchangeRateStrategyBase : IExchangeRateStrategy
	{
		public async Task<decimal> Convert(decimal amount, string amountCurrencyCode, string currencyCodeToConvert, DateTime date)
		{
			var rate = await GetRate(amountCurrencyCode, currencyCodeToConvert, date);
			return amount * rate;
		}

		protected abstract Task<decimal> GetRate(string currencyCodeFrom, string currencyCodeTo, DateTime date);

        
        public class SpecificDateExchangeRateStrategy : ExchangeRateStrategyBase {

            private readonly IRatesService _ratesService;

            public SpecificDateExchangeRateStrategy(IRatesService ratesService) {
                _ratesService = ratesService;
            }
            protected override async Task<decimal> GetRate(string currencyCodeFrom, string currencyCodeTo, DateTime date) {
                var rates = await _ratesService.GetRatesByDate(date);

                var rateFrom = rates.FirstOrDefault(x => x.CurrencyCode == currencyCodeFrom)?.Rate ?? 0;
                var rateTo = rates.FirstOrDefault(x => x.CurrencyCode == currencyCodeTo)?.Rate ?? 0;

                if (rateFrom == 0 || rateTo == 0)
                    throw new Exception("No rate found for the selected date!");

                return rateTo / rateFrom;
            }
        }
        
        public class SpecificDateOrNextAvailableRateStrategy : ExchangeRateStrategyBase
        {
            private readonly IRatesService _ratesService;

            public SpecificDateOrNextAvailableRateStrategy(IRatesService ratesService)
            {
                _ratesService = ratesService;
            }
            protected override async Task<decimal> GetRate(string currencyCodeFrom, string currencyCodeTo, DateTime date) {
                IList<CurrencyRateEntity> rates;
                
                rates = await _ratesService.GetRatesByDate(date);

                if (rates.Count == 0) {
                    rates = await _ratesService.GetRatesAfterSpecificDate(date);
                }
                var rateFrom = rates.FirstOrDefault(x => x.CurrencyCode == currencyCodeFrom)?.Rate ?? 0;
                var rateTo = rates.FirstOrDefault(x => x.CurrencyCode == currencyCodeTo)?.Rate ?? 0;

                if (rateFrom == 0 || rateTo == 0)
                    throw new Exception("No rate found for the selected date or dates after!");

                return rateTo / rateFrom;
            }
        }

    }
}
