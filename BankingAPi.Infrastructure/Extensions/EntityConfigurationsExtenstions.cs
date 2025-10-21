using BankingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPi.Infrastructure.Extensions;

public static class EntityConfigurationsExtenstions
{
    public static void HasBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
    where TEntity : BaseEntity
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd()
            .IsRequired()
            .HasComment("Primary key. Is Auto Generated");
    }
}