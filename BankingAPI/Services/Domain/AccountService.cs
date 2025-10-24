using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BankingAPI.Common.Models.Identity;
using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure;
using BankingAPI.Interfaces.Domain;
using BankingAPI.Options;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BankingAPI.Services.Domain;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly IdentityOptions _identityOptions;

    public AccountService(ApplicationDbContext context, IOptions<IdentityOptions> identityOptions)
    {
        _context = context;
        _identityOptions = identityOptions.Value;
    }

    public async Task RegisterAsync(IdentityUser user)
    {
        var emailTaken = await _context.IdentityUsers.AsNoTracking()
            .AnyAsync(x => x.Email.ToLower() == user.Email.ToLower());

        if (emailTaken)
        {
            //todo: add custom exceptions 
            throw new Exception("Email is already taken");
        }

        user.Password = BCryptHelper.HashPassword(user.Password, BCryptHelper.GenerateSalt());
        
        await _context.IdentityUsers.AddAsync(user);
        
        //todo: move User role name to constraints
        var userRole = await _context.IdentityRoles.AsNoTracking().FirstAsync(x => x.RoleName == "User");

        var defaultRole = new IdentityUserRole()
        {
            UserId = user.Id,
            RoleId = userRole.Id
        };

        await _context.IdentityUserRoles.AddAsync(defaultRole);
    }

    public async Task LoginAsync(LoginDto model)
    {
        var client = await _context.IdentityClients.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId == model.ClientId)
                     ?? throw new Exception("Invalid Client");

        var user = await _context.IdentityUsers.AsNoTracking()
                       .Include(x => x.UserRoles)!
                       .ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Email.ToLower() == model.Email)
                   ?? throw new Exception("Invalid credentials");

        var isPasswordValid = BCryptHelper.CheckPassword(model.Password, user.Password);

        if (!isPasswordValid)
        {
            throw new Exception("Invalid Credentials");
        }

        var token = GenerateToken(user, client);

    }

    private async Task<string> GenerateToken(IdentityUser user, IdentityClient client)
    {
        var signingKey = await _context.IdentitySigningKeys.AsNoTracking().FirstOrDefaultAsync(x => x.IsActive)
                         ?? throw new Exception("No signing key available");

        var privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey);

        var rsa = RSA.Create();
        
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

        var rsaSecurityKey = new RsaSecurityKey(rsa)
        {
            KeyId = signingKey.KeyId
        };

        var credentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);
        
        // Initialize a list of claims to include in the JWT
        var claims = new List<Claim>
        {
            // Subject (sub) claim with the user's ID
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            // JWT ID (jti) claim with a unique identifier for the token
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // Name claim with the user's first name
            new(ClaimTypes.Name, user.FirstName),
            // NameIdentifier claim with the user's email
            new(ClaimTypes.NameIdentifier, user.Email),
            // Email claim with the user's email
            new(ClaimTypes.Email, user.Email)
        };
        // Iterate through the user's roles and add each as a Role claim
        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role!.RoleName));
        }
        // Define the JWT token's properties, including issuer, audience, claims, expiration, and signing credentials
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _identityOptions.Issuer, // The token issuer, typically your application's URL
            audience: client.ClientUrl, // The intended recipient of the token, typically the client's URL
            claims: claims, // The list of claims to include in the token
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time set to 1 hour from now
            signingCredentials: credentials // The credentials used to sign the token
        );
        // Create a JWT token handler to serialize the token
        var tokenHandler = new JwtSecurityTokenHandler();
        // Serialize the token to a string
        var token = tokenHandler.WriteToken(tokenDescriptor);
        // Return the serialized JWT token
        return token;
    }
}