namespace BankingAPI.Common.Models.Wallet;

public class DepositFundsDto
{
    public required string WalletNumber { get; set; }

    public double Amount { get; set; }
}