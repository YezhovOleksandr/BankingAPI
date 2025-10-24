using BankingAPI.Domain.Enums;

namespace BankingAPI.Common.Models.UserWallet;

public class UserWalletDto
{
    public required string Id { get; set; }
    
    public double Amount { get; set; }
    
    public required string WalletNumber { get; set; }
    
    public CurrencyType CurrencyType { get; set; }
}