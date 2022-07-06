using System.Threading.Tasks;

namespace CodeCanvas.Services
{
	public interface IWalletAdjustmentService
	{
		Task<decimal> AdjustBalance(string exchangeRateStrategy, int walletId, string currencyCode, decimal amount);
	}

	class WalletAdjustmentService : IWalletAdjustmentService
	{
		public Task<decimal> AdjustBalance(string exchangeRateStrategy, int walletId, string currencyCode, decimal amount)
		{
			// todo: implement WalletAdjustmentService.AdjustBalance()

			// choose the corresponding IExchangeRateStrategy based on exchangeRateStrategy
			// use IExchangeRateStrategy.Convert() to convert the amount into the currency of the wallet
			// bring WalletEntity and call Adjust() to adjust the balance
			// persist the changes
			// retun new balance

			throw new System.NotImplementedException();
		}
	}
}
