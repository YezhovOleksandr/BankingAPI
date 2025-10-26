using BankingAPI.Common.Models.Wallet;
using BankingAPI.Domain.Entities.UserWallet;

namespace BankingAPI.Interfaces.Domain;

public interface IWalletService
{
    Task DepositFundsAsync(DepositFundsDto model);
    
    Task<UserWalletEntity?> GetByIdAsync(Guid id);
    
    Task<UserWalletEntity?> GetByUserIdAsync(Guid id);
    
    Task<UserWalletEntity?> GetByWalletNumberAsync(string number);
    
    Task TransferFundsAsync(TransferFundsDto model, string userId);
}