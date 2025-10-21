namespace BankingAPI.Domain.Entities.Identity;

public class IdentityRole : BaseEntity
{
    public required string RoleName { get; set; }

    #region Nav Props

    public List<IdentityUserRole>? UserRoles { get; set; } = [];

    #endregion
}