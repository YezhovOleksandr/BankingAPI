using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPi.Infrastructure.Configurations;

public class IdentitySigningKeyEntityConfigurations : IEntityTypeConfiguration<IdentitySigningKey>
{
    public void Configure(EntityTypeBuilder<IdentitySigningKey> builder)
    {
        builder.HasBaseEntity();

        builder.Property(x => x.KeyId).HasMaxLength(100).IsRequired();

        builder.Property(x => x.PrivateKey).IsRequired();

        builder.Property(x => x.PublicKey).IsRequired();

        builder.Property(x => x.IsActive).IsRequired();

        builder.Property(x => x.ExpiresAt).IsRequired();
    }
}