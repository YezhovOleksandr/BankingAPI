using BankingAPI.Common.Models.Identity;
using BankingAPI.Interfaces.Api;
using BankingAPI.Interfaces.Domain;
using BankingAPI.Mappers.Identity;

namespace BankingAPI.Services.Api;

public class AccountApiService : IAccountApiService
{
    private readonly IAccountService _accountService;

    public AccountApiService(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task RegisterAsync(RegisterDto model)
    {
        var user = model.ToDomain();

        if (user is null)
        {
            //todo: add custom exception
            throw new Exception("Invalid input data");
        }

        await _accountService.RegisterAsync(user);
    }

    public async Task<string> LoginAsync(LoginDto model)
    {
        var token = await _accountService.LoginAsync(model);
        
        return token;
    }
}