using BankingAPI.Domain.Entities.Identity;

namespace BankingAPI.Interfaces.Domain;

public interface IUserService
{
    Task<List<IdentityUser>> GetAllUsers();
}