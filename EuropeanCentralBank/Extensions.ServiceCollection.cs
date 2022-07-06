using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace EuropeanCentralBank
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEuropeanCentralBank(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IEuropeanCentralBankClient, EuropeanCentralBankClient>();
			services.Configure<EuropeanCentralBankSettings>(configuration.GetSection(EuropeanCentralBankSettings.settings));

			return services;
		}
	}
}
