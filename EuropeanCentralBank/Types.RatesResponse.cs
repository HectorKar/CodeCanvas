using System;
using System.Collections.Generic;

namespace EuropeanCentralBank
{
	public class RatesResponse
	{
		public DateTime Date { get; }
		public IReadOnlyCollection<CurrencyRate> Rates { get; }

		public RatesResponse(DateTime date, IReadOnlyCollection<CurrencyRate> rates)
		{
			Date = date;
			Rates = rates;
		}
	}
}
