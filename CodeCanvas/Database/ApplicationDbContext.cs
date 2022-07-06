#nullable disable
using System.Reflection;
using CodeCanvas.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCanvas.Database
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<CurrencyRateEntity> CurrencyRates { get; set; }
		public DbSet<WalletEntity> Wallets { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(builder);
		}
	}
}
