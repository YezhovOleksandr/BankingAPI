using BankingAPI.Domain.Entities.Identity;
using BankingAPI.Domain.Exceptions;
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
        return await _context.IdentityUsers.AsNoTracking().Include(x => x.UserWallets).ToListAsync();
    }

    public async Task<IdentityUser?> GetUserById(string id)
    {
        return await _context.IdentityUsers.AsNoTracking().Include(x => x.UserWallets)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id)) ??
               throw new NotFoundException($"User with id: {id} was not found");
    }

    public async Task<IdentityUser> GetUserByWalletNumber(string walletNumber)
    {
        var userId = (await _context.UserWallets.AsNoTracking().FirstOrDefaultAsync(x => x.WalletNumber == walletNumber) ?? throw new NotFoundException("Wallet was not found"))
            .UserId;
        
        return await _context.IdentityUsers.AsNoTracking().Include(x => x.UserWallets).FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User was not found");
    }
}