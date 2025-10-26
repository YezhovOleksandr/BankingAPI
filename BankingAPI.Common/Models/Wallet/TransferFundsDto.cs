namespace BankingAPI.Common.Models.Wallet;

public class TransferFundsDto
{
    public required string WalletNumber { get; set; }

    public required double Amount { get; set; }
}