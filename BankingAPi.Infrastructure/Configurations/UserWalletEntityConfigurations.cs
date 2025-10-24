using BankingAPI.Domain.Entities.UserWallet;
using BankingAPi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPi.Infrastructure.Configurations;

public class UserWalletEntityConfigurations : IEntityTypeConfiguration<UserWalletEntity>
{
    public void Configure(EntityTypeBuilder<UserWalletEntity> builder)
    {
        builder.HasBaseEntity();
        
        builder.Property(x => x.UserId).IsRequired();
        
        builder.Property(x => x.Amount).IsRequired();
        
        builder.Property(x => x.CurrencyType).IsRequired();
        
        builder.Property(x => x.WalletNumber).IsRequired();
        
        builder.HasIndex(x => x.WalletNumber).IsUnique();

        builder.HasOne(x => x.IdentityUser)
            .WithMany(x => x.UserWallets)
            .HasForeignKey(x => x.UserId);
    }
}