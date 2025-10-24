using BankingAPI.Common.Models;
using BankingAPI.Extensions;
using BankingAPI.Interfaces.Api;
using BankingAPI.Interfaces.Domain;
using BankingAPI.Mappers.Users;

namespace BankingAPI.Services.Api;

public class UserApiService : IUserApiService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public UserApiService(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<UserDto?>> GetUsers()
    {
        var requestUserId = GetUserId();
        var users = await _userService.GetAllUsers();

        return users.Select(x => x.ToDto(requestUserId)).ToList();
    }

    public async Task<UserDto?> GetUserById(string id)
    {
        var requestUserId = GetUserId();
        
        var user = await _userService.GetUserById(id);
        
        return user?.ToDto(requestUserId);
    }

    public async Task<UserDto?> GetUserByWalletNumber(string walletNumber)
    {
        var requestUserId = GetUserId();
        
        var user = await _userService.GetUserByWalletNumber(walletNumber);
        
        return user.ToDto(requestUserId);
    }

    private string? GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.Sub() ?? string.Empty;
    }
}