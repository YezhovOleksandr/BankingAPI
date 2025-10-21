using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPi.Infrastructure.Configurations;

public class IdentityClientEntityConfigurations : IEntityTypeConfiguration<IdentityClient>
{
    public void Configure(EntityTypeBuilder<IdentityClient> builder)
    {
        builder.HasBaseEntity();
        
        builder.HasIndex(x => x.ClientId).IsUnique();
        
        builder.Property(x => x.ClientId).HasMaxLength(50).IsRequired();
        
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        
        builder.Property(x => x.ClientSecret).HasMaxLength(100).IsRequired();
        
        builder.Property(x => x.ClientUrl).HasMaxLength(200).IsRequired();

        builder.Property(x => x.IsActive).IsRequired();
    }
}