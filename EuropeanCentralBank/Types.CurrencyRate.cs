namespace EuropeanCentralBank
{
	public class CurrencyRate
	{
		public string CurrencyCode { get; }
		public decimal Rate { get; }

		public CurrencyRate(string currencyCode, decimal rate)
		{
			CurrencyCode = currencyCode;
			Rate = rate;
		}
	}
}
