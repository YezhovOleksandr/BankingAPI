using BankingAPi.Infrastructure;
using BankingAPi.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(nameof(DatabaseOptions)));

builder.Services.AddDbContext<ApplicationDbContext>
(
    (serviceProvider, opt) =>
    {
        var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
        opt.UseSqlServer(dbOptions.ConnectionString);
        opt.EnableDetailedErrors();
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}