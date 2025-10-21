namespace BankingAPI.Domain.Entities.Identity;

public class IdentityClient : BaseEntity
{
    public required string ClientId { get; set; }

    public required string Name { get; set; }

    public required string ClientSecret { get; set; }

    public required string ClientUrl { get; set; }

    public required bool IsActive { get; set; }

    #region Nav Props

    public List<IdentityRefreshToken>? RefreshTokens { get; set; } = [];

    #endregion
}