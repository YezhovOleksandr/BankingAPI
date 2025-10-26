using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure;
using BankingAPI.Options;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Extensions;

public static class DbInitializerExtensions
{
    public static async Task InitDb(ApplicationDbContext context, IConfiguration configuration)
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

        await context.SaveChangesAsync();

        var admin = await context.IdentityRoles.FirstOrDefaultAsync(x => x.RoleName == "Admin");
        var adminUser = new IdentityUser()
        {
            FirstName = "admin",
            LastName = "admin",
            Email = "admin@admin.com",
            Password = "Admin123!",
            PhoneNumber = "0123456789",
            Username = "admin",
        };
        adminUser.Password = BCryptHelper.HashPassword(adminUser.Password, BCryptHelper.GenerateSalt());
        await context.IdentityUsers.AddAsync(adminUser);

        await context.IdentityUserRoles.AddAsync(new IdentityUserRole()
        {
            RoleId = admin.Id,
            UserId = adminUser.Id
        });
        
        await context.SaveChangesAsync();
    }
}