﻿namespace BankingAPI.Domain.Entities.Identity;

public class IdentityUser : BaseEntity
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }
}