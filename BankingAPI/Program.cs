using System.IdentityModel.Tokens.Jwt;
using BankingAPi.Infrastructure;
using BankingAPi.Infrastructure.Extensions;
using BankingAPi.Infrastructure.Options;
using BankingAPI.Interfaces.Api;
using BankingAPI.Interfaces.Domain;
using BankingAPI.Middlewares;
using BankingAPI.Services.Api;
using BankingAPI.Services.Background;
using BankingAPI.Services.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IdentityOptions = BankingAPI.Options.IdentityOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(nameof(DatabaseOptions)));
builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(BankingAPI.Options.IdentityOptions)));

builder.Services.AddDbContext<ApplicationDbContext>
(
    (serviceProvider, opt) =>
    {
        var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
        opt.UseSqlServer(dbOptions.ConnectionString);
        opt.EnableDetailedErrors();
    }
);

builder.Services.AddControllers();//addJsonOptions
builder.Services.AddHostedService<KeyRotationService>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
    JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    var identityOptions = new IdentityOptions();
    builder.Configuration.GetSection(nameof(IdentityOptions)).Bind(identityOptions);
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

            var options = builder.Configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>();
            
            var jwks = httpClient.GetStringAsync($"{options.Issuer}/.well-known/jwks.json").Result;
            // Parse the fetched JWKS into a JsonWebKeySet object
            var keys = new JsonWebKeySet(jwks);

            return keys.Keys;
        }
    };
});

builder.Services.AddScoped<IAccountApiService, AccountApiService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserApiService, UserApiService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IWalletApiService, WalletApiService>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
DbInitializerExtensions.InitDb(context);

app.Run();
