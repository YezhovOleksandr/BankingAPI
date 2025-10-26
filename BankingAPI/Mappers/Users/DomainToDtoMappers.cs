using BankingAPI.Common.Models;
using BankingAPI.Common.Models.Identity;
using BankingAPI.Common.Models.UserWallet;
using BankingAPI.Domain.Entities.Identity;
using BankingAPI.Domain.Entities.UserWallet;

namespace BankingAPI.Mappers.Users;

public static class DomainToDtoMappers
{
    public static UserDto? ToDto(this IdentityUser? entity, string? userId) =>
        entity == null
            ? null
            : new UserDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Username = entity.Username,
                UserWallets = entity.UserWallets?.Select(x => x.ToDto(userId)).ToList()
            };

    public static UserWalletDto? ToDto(this UserWalletEntity? entity, string? userId) =>
        entity == null
            ? null
            : entity.UserId.ToString() == userId || userId == null
                ? new UserWalletDto()
                {
                    Id = entity.Id.ToString(),
                    WalletNumber = entity.WalletNumber,
                    Amount = entity.Amount,
                    CurrencyType = entity.CurrencyType,
                }
                : new UserWalletDto()
                {
                    Id = entity.Id.ToString(),
                    WalletNumber = entity.WalletNumber
                };
}