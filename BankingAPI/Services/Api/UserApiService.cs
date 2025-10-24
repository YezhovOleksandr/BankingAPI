using BankingAPI.Common.Models;
using BankingAPI.Interfaces.Api;
using BankingAPI.Interfaces.Domain;
using BankingAPI.Mappers.Users;

namespace BankingAPI.Services.Api;

public class UserApiService : IUserApiService
{
    private readonly IUserService _userService;

    public UserApiService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<List<UserDto?>> GetUsers()
    {
        var users = await _userService.GetAllUsers();

        return users.Select(x => x.ToDto()).ToList();
    }
}