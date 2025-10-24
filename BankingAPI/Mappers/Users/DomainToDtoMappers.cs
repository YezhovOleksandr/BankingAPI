using BankingAPI.Common.Models;
using BankingAPI.Common.Models.Identity;
using BankingAPI.Domain.Entities.Identity;

namespace BankingAPI.Mappers.Users;

public static class DomainToDtoMappers
{
    public static UserDto? ToDto(this IdentityUser? entity) =>
        entity == null
            ? null
            : new UserDto()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Username = entity.Username
            };
}