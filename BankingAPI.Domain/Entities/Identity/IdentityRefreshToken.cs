namespace BankingAPI.Domain.Entities.Identity;

public class IdentityRefreshToken : BaseEntity
{
    public required string Token { get; set; } 

    public required string JwtId { get; set; } 
    
    public required DateTime Expires { get; set; }
    
    public bool IsRevoked { get; set; }
    
    public DateTime? RevokedAt { get; set; }
    
    public required Guid UserId { get; set; }
    
    public Guid ClientId { get; set; }
    
    #region Nav Properties
    
    public IdentityUser? User { get; set; }
    
    public IdentityClient? Client { get; set; }
    #endregion
}