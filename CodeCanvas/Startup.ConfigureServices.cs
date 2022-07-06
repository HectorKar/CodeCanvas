using CodeCanvas.Database;
using CodeCanvas.HostedServices;
using CodeCanvas.Services;
using EuropeanCentralBank;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeCanvas
{
	public partial class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddSwaggerDocument();

			services.AddDbContext<ApplicationDbContext>(options =>
			{
				var connectionString = Configuration.GetConnectionString(Constants.Databases.Application);
				options.UseSqlite(connectionString);
			});

			services.AddEuropeanCentralBank(Configuration);
            services.AddScoped<IRatesService, RatesService>();
			services.AddHostedService<UpdateRatesHostedService>();
			// todo: register UpdateRatesHostedService
		}
	}
}
