using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Common.Models.Identity;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "ClientId is required.")]
    public required string ClientId { get; set; }
}