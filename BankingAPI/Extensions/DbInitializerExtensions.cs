using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure;
using BankingAPI.Options;

namespace BankingAPI.Extensions;

public static class DbInitializerExtensions
{
    public static void InitDb(ApplicationDbContext context, IConfiguration configuration)
    {
        var identityOptions = new IdentityOptions();
        configuration.GetSection(nameof(IdentityOptions)).Bind(identityOptions);
        if (context.IdentityClients.Any() || context.IdentityRoles.Any())
        {
            return;
        }
        context.IdentityClients.AddRange([new IdentityClient()
            {
                ClientId = "Client1",
                Name = "Client Application 1",
                ClientUrl = $"{identityOptions.Audience}",
                ClientSecret = "secret",
                IsActive = true
            },
            new IdentityClient()
            {
                ClientId = "Client2",
                Name = "Client Application 2",
                ClientUrl = $"{identityOptions.Audience}",
                ClientSecret = "secret",
                IsActive = true
            }]);
        
        context.IdentityRoles.AddRange([new IdentityRole() {RoleName = "Admin" },
            new IdentityRole() { RoleName = "User"}]);
        
        context.SaveChanges();
    }
}