using BankingAPI.Extensions;
using BankingAPi.Infrastructure;
using BankingAPi.Infrastructure.Extensions;
using BankingAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddOptions(builder.Configuration);
builder.Services.AddSqlDatabase();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddServices().AddApiServices().AddHostedServices();

builder.Services.AddControllers();

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
