using System.Security.Cryptography;
using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Services.Background;

public class KeyRotationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _rotationInterval = TimeSpan.FromDays(7);
    public KeyRotationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await RotateKeysAsync();
            await Task.Delay(_rotationInterval, stoppingToken);
        }
    }
    private async Task RotateKeysAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var activeKey = await context.IdentitySigningKeys.FirstOrDefaultAsync(k => k.IsActive);
        if (activeKey == null || activeKey.ExpiresAt <= DateTime.UtcNow.AddDays(10))
        {
            if (activeKey != null)
            {
                activeKey.IsActive = false;
                context.IdentitySigningKeys.Update(activeKey);
            }
            using var rsa = RSA.Create(2048);
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            var newKeyId = Guid.NewGuid().ToString();
            var newKey = new IdentitySigningKey()
            {
                KeyId = newKeyId,
                PrivateKey = privateKey,
                PublicKey = publicKey,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddYears(1) // Set the new key to expire in one year.
            };
            await context.IdentitySigningKeys.AddAsync(newKey);
            await context.SaveChangesAsync();
        }
    }
}