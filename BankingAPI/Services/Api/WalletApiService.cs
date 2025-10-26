using BankingAPI.Common.Models.UserWallet;
using BankingAPI.Common.Models.Wallet;
using BankingAPI.Extensions;
using BankingAPI.Interfaces.Api;
using BankingAPI.Interfaces.Domain;
using BankingAPI.Mappers.Users;

namespace BankingAPI.Services.Api;

public class WalletApiService : IWalletApiService
{
    private readonly IWalletService _walletService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public WalletApiService(IWalletService walletService, IHttpContextAccessor httpContextAccessor)
    {
        _walletService = walletService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task DepositMoneyAsync(DepositFundsDto model, string walletNumber)
    {
        model.WalletNumber = walletNumber;
        await _walletService.DepositFundsAsync(model);
    }

    public async Task TransferMoneyAsync(TransferFundsDto model)
    {
        var userId = GetUserId();
        
        var initialWallet = await _walletService.GetByUserIdAsync(Guid.Parse(userId));

        if (initialWallet.WalletNumber == model.WalletNumber)
        {
            throw new Exception("You cannot send money to the same wallet");
        }

        await _walletService.TransferFundsAsync(model, userId);
    }

    public async Task<UserWalletDto?> GetByIdAsync(string id)
    {
        var userId = GetUserId();
        
        var wallet = await _walletService.GetByIdAsync(Guid.Parse(id));

        return wallet.ToDto(userId);
    }

    private string? GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.Sub() ?? string.Empty;
    }
}