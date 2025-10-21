using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPi.Infrastructure.Configurations;

public class IdentityRefreshTokenConfigurations : IEntityTypeConfiguration<IdentityRefreshToken>
{
    public void Configure(EntityTypeBuilder<IdentityRefreshToken> builder)
    {
        builder.HasBaseEntity();
        
        builder.HasIndex(x => x.Token).IsUnique();
        
        builder.Property(x => x.Token).IsRequired();
        
        builder.Property(x => x.JwtId).IsRequired();
        
        builder.Property(x => x.Expires).IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId);
        
        builder.HasOne(x => x.Client)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.ClientId);
    }
}