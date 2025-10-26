using BankingAPI.Common.Models.UserWallet;
using BankingAPI.Common.Models.Wallet;

namespace BankingAPI.Interfaces.Api;

public interface IWalletApiService
{
    Task DepositMoneyAsync(DepositFundsDto model, string walletNumber);
    
    Task TransferMoneyAsync(TransferFundsDto model, string walletNumber);
    
    Task<UserWalletDto?> GetByIdAsync(string id);
}