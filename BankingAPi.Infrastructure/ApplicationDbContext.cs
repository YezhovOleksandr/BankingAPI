using BankingAPI.Domain.Entities;
using BankingAPI.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BankingAPi.Infrastructure;

public class ApplicationDbContext : DbContext
{

    public DbSet<IdentityUser> IdentityUsers { get; set; }

    public DbSet<IdentityRole> IdentityRoles { get; set; }

    public DbSet<IdentityClient> IdentityClients { get; set; }

    public DbSet<IdentityUserRole> IdentityUserRoles { get; set; }

    public DbSet<IdentityRefreshToken> IdentityRefreshTokens { get; set; }
    
    public DbSet<IdentitySigningKey> IdentitySigningKeys { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = new())
    {
        SetTimestamps(entity);
        return base.AddAsync(entity, cancellationToken);
    }
    
    private static void SetTimestamps(object entity)
    {
        if (entity is not BaseEntity baseEntity) return;
        var now = DateTime.UtcNow;

        baseEntity.CreatedAt ??= now;
        baseEntity.UpdatedAt ??= now;
    }
}