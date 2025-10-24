using BankingAPI.Common.Models.Identity;
using BankingAPI.Domain.Entities.Identity;

namespace BankingAPI.Mappers.Identity;

public static class DtoToDomainMappers
{
    public static IdentityUser? ToDomain(this RegisterDto? model) =>
        model == null
            ? null
            : new IdentityUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Username = model.Username
            };
}