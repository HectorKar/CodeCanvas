using System;

namespace CodeCanvas
{
	public class NoSufficientBalanceException : Exception
	{
		public int WalletId { get; }
		public decimal RequestedAmout { get; }
		public decimal AvailableBalance { get; }

		public NoSufficientBalanceException(int walletId, decimal requestedAmout, decimal availableBalance)
		{
			WalletId = walletId;
			RequestedAmout = requestedAmout;
			AvailableBalance = availableBalance;
		}
	}
}
