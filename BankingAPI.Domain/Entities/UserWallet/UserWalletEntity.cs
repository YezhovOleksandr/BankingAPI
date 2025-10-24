using BankingAPI.Domain.Entities.Identity;
using BankingAPI.Domain.Enums;

namespace BankingAPI.Domain.Entities.UserWallet;

public class UserWalletEntity : BaseEntity
{
    public required string WalletNumber { get; set; }

    public required CurrencyType CurrencyType { get; set; }

    public Guid UserId { get; set; }

    public double Amount { get; set; }

    #region Nav Props

    public IdentityUser? IdentityUser { get; set; }

    #endregion
}