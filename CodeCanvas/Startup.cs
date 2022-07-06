using Microsoft.Extensions.Configuration;
using Serilog;

namespace CodeCanvas
{
	public partial class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
		}
	}
}
