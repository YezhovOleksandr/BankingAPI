namespace BankingAPI.Domain.Entities.Identity;

public class IdentitySigningKey : BaseEntity
{
    public required string KeyId { get; set; }

    public required string PrivateKey { get; set; }

    public required string PublicKey { get; set; }

    public required bool IsActive { get; set; }

    public required DateTime ExpiresAt { get; set; }
}