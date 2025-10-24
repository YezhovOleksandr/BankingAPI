using BankingAPI.Common.Models;

namespace BankingAPI.Interfaces.Api;

public interface IUserApiService
{
    Task<List<UserDto?>> GetUsers();
}