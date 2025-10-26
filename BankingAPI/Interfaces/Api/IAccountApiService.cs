using BankingAPI.Common.Models.Identity;

namespace BankingAPI.Interfaces.Api;

public interface IAccountApiService
{
    Task RegisterAsync(RegisterDto model);

    Task<TokenResponseDto> LoginAsync(LoginDto model);
}