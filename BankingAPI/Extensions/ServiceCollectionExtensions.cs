using System.IdentityModel.Tokens.Jwt;
using BankingAPi.Infrastructure;
using BankingAPi.Infrastructure.Options;
using BankingAPI.Interfaces.Api;
using BankingAPI.Interfaces.Domain;
using BankingAPI.Options;
using BankingAPI.Services.Api;
using BankingAPI.Services.Background;
using BankingAPI.Services.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BankingAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));
        services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));
        
        return services;
    }

    public static IServiceCollection AddSqlDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>
        (
            (serviceProvider, opt) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                opt.UseSqlServer(dbOptions.ConnectionString);
                opt.EnableDetailedErrors();
            }
        );

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var identityOptions = new IdentityOptions();
            configuration.GetSection(nameof(IdentityOptions)).Bind(identityOptions);
            opt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = identityOptions.Issuer,
                ValidAudience = identityOptions.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                {
                    var httpClient = new HttpClient();

                    var options = configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>();
            
                    var jwks = httpClient.GetStringAsync($"{options.Issuer}/.well-known/jwks.json").Result;
                    // Parse the fetched JWKS into a JsonWebKeySet object
                    var keys = new JsonWebKeySet(jwks);

                    return keys.Keys;
                }
            };
        });

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWalletService, WalletService>();

        return services;
    }
    
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountApiService, AccountApiService>();
        services.AddScoped<IUserApiService, UserApiService>();
        services.AddScoped<IWalletApiService, WalletApiService>();

        return services;
    }  
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<KeyRotationService>();

        return services;
    }
}