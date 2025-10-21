namespace BankingAPI.Domain.Entities.Identity;

public class IdentityUserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    #region Nav Props

    public IdentityUser? User { get; set; }
    public IdentityRole? Role { get; set; }

    #endregion
}