using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CodeCanvas.Database
{
	public class MigrationsContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Path.GetFullPath(@"."))
				.AddJsonFile("appsettings.json")
				.AddJsonFile("appsettings.development.json", optional: true)
				.Build();

			var connectionString = configuration.GetConnectionString(Constants.Databases.Application);

			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
			builder.UseSqlite(connectionString);

			return new ApplicationDbContext(builder.Options);
		}
	}
}
