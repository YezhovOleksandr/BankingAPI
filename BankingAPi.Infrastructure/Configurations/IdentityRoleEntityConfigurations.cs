using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPi.Infrastructure.Configurations;

public class IdentityRoleEntityConfigurations : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasBaseEntity();

        builder.Property(x => x.RoleName).IsRequired();
    }
}