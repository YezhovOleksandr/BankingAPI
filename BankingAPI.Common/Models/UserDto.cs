using BankingAPI.Common.Models.UserWallet;

namespace BankingAPI.Common.Models;

public class UserDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }

    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public List<UserWalletDto> UserWallets { get; set; } = [];
}