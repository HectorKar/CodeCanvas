using CodeCanvas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeCanvas.Database.Configuration
{
	class WalletConfiguration : IEntityTypeConfiguration<WalletEntity>
	{
		public void Configure(EntityTypeBuilder<WalletEntity> builder)
		{
			builder.ToTable("Wallets");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.CurrencyCode).IsRequired();
			builder.Property(x => x.Balance).IsRequired();
			builder.Property(x => x.CreatedAt).IsRequired();
			builder.Property(x => x.UpdatedAt).IsRequired();
		}
	}
}
