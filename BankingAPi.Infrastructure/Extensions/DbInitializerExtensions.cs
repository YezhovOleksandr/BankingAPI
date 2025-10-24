using BankingAPI.Domain.Entities.Identity;

namespace BankingAPi.Infrastructure.Extensions;

public static class DbInitializerExtensions
{
    public static void InitDb(ApplicationDbContext context)
    {
        if (context.IdentityClients.Any() || context.IdentityRoles.Any())
        {
            return;
        }
        context.IdentityClients.AddRange([new IdentityClient()
            {
                ClientId = "Client1",
                Name = "Client Application 1",
                ClientUrl = "https://client1.com",
                ClientSecret = "secret",
                IsActive = true
            },
            new IdentityClient()
            {
                ClientId = "Client2",
                Name = "Client Application 2",
                ClientUrl = "https://client2.com",
                ClientSecret = "secret",
                IsActive = true
            }]);
        
        context.IdentityRoles.AddRange([new IdentityRole() {RoleName = "Admin" },
            new IdentityRole() { RoleName = "User"}]);
        
        context.SaveChanges();
    }
}