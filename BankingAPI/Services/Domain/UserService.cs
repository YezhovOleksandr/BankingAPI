using BankingAPI.Domain.Entities.Identity;
using BankingAPi.Infrastructure;
using BankingAPI.Interfaces.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Services.Domain;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<IdentityUser>> GetAllUsers()
    {
        return await _context.IdentityUsers.AsNoTracking().ToListAsync();
    }
}