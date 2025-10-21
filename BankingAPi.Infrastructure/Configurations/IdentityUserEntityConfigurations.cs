using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPi.Infrastructure.Configurations;

public class IdentityUserEntityConfigurations : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasBaseEntity();
        
        builder.Property(x => x.FirstName).IsRequired();
        
        builder.Property(x => x.LastName).IsRequired();
        
        builder.Property(x => x.Email).IsRequired();
        
        builder.Property(x => x.Password).IsRequired();
        
        builder.Property(x => x.PhoneNumber).IsRequired();
        
        builder.Property(x => x.Username).IsRequired();
    }
}