using System;

namespace CodeCanvas.Entities
{
	public class WalletEntity
	{
		/// <summary>
		/// wallet id
		/// </summary>
		public int Id { get; }
		/// <summary>
		///  currency of the wallet
		/// </summary>
		public string CurrencyCode { get; }
		/// <summary>
		/// available balance of the wallet
		/// </summary>
		public decimal Balance { get; private set; }
		/// <summary>
		/// creation date of the wallet
		/// </summary>
		public DateTime CreatedAt { get; }
		/// <summary>
		/// date of the last update of the wallet
		/// </summary>
		public DateTime UpdatedAt { get; private set; }

		public WalletEntity(int id, string currencyCode, decimal balance, DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			CurrencyCode = currencyCode;
			Balance = balance;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public void Adjust(decimal amount)
		{
			if (Balance + amount < 0)
				throw new NoSufficientBalanceException(Id, amount, Balance);

			Balance += amount;
			UpdatedAt = DateTime.UtcNow;
		}
	}
}
