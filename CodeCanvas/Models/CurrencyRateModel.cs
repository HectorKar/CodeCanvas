using System;

namespace CodeCanvas.Models
{
	public class CurrencyRateModel
	{
		public int Id { get; }
		public string CurrencyCode { get; }
		public decimal Rate { get; }
		public DateTime CreatedAt { get; }

		public CurrencyRateModel(int id, string currencyCode, decimal rate, DateTime createdAt)
		{
			Id = id;
			CurrencyCode = currencyCode;
			Rate = rate;
			CreatedAt = createdAt;
		}
	}
}
