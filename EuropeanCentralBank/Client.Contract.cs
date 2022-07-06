using System.Threading.Tasks;

namespace EuropeanCentralBank
{
	public interface IEuropeanCentralBankClient
	{
		Task<RatesResponse> GetRates();
	}
}
