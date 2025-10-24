using BankingAPI.Common.Models.Identity;
using BankingAPI.Domain.Entities.Identity;

namespace BankingAPI.Interfaces.Domain;

public interface IAccountService
{
    Task RegisterAsync(IdentityUser user);

    Task LoginAsync(LoginDto model);
}