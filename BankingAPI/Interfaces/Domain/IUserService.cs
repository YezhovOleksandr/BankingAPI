using BankingAPI.Domain.Entities.Identity;

namespace BankingAPI.Interfaces.Domain;

public interface IUserService
{
    Task<List<IdentityUser>> GetAllUsers();

    Task<IdentityUser?> GetUserById(string id);
    
    Task<IdentityUser> GetUserByWalletNumber(string walletNumber);
}