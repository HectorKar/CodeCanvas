using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeCanvas.Database;
using CodeCanvas.Entities;
using EuropeanCentralBank;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CodeCanvas.HostedServices
{
	public class UpdateRatesHostedService : IHostedService, IDisposable
	{
		private readonly ILogger<UpdateRatesHostedService> _logger;
		private readonly IServiceProvider _serviceProvider;
		private Timer? _timer;

		public UpdateRatesHostedService(ILogger<UpdateRatesHostedService> logger, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
			//_db = db;
		}

		public Task StartAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("UpdateRatesHostedService running.");

			_timer = new Timer(UpdateRates, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

			return Task.CompletedTask;
		}

		private void UpdateRates(object? state)
		{
			using (IServiceScope scope = _serviceProvider.CreateScope())
			{
				IEuropeanCentralBankClient scopedProcessingService =
					scope.ServiceProvider.GetRequiredService<IEuropeanCentralBankClient>();

				var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


				var ratesRespone = scopedProcessingService.GetRates().GetAwaiter().GetResult().Rates.Select(x => new CurrencyRateEntity(0, x.CurrencyCode, x.Rate, DateTime.UtcNow, DateTime.UtcNow)); ;
				var currencyRates = dbContext.CurrencyRates.Where(x => x.CreatedAt.Date == DateTime.UtcNow.Date).ToList();
				//var currencyRatesResponse = ratesRespone.Rates.Select(x => new CurrencyRateEntity(0, x.CurrencyCode, x.Rate, DateTime.UtcNow, DateTime.UtcNow));

				if (currencyRates.Count == 0)
				{
					dbContext.CurrencyRates.AddRange(ratesRespone);
					dbContext.SaveChanges();
                }
                else
                {
                    foreach (var rateResponse in ratesRespone) {
                        var currencyRate = currencyRates.FirstOrDefault(x => x.CurrencyCode == rateResponse.CurrencyCode);

                        if (currencyRate == null)
                            dbContext.CurrencyRates.Add(rateResponse);
                        else {
                            if (currencyRate.Rate != rateResponse.Rate) {
								currencyRate.Update(rateResponse.Rate);
                            }
                        }
                    }
                    dbContext.SaveChanges();
                }


			}

			_logger.LogInformation("UpdateRatesHostedService rates updated.");

		}

		public Task StopAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("UpdateRatesHostedService is stopping.");

			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
