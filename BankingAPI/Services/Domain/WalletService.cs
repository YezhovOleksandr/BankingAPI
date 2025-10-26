using System.Transactions;
using BankingAPI.Common.Models.Wallet;
using BankingAPI.Domain.Entities.UserWallet;
using BankingAPI.Domain.Exceptions;
using BankingAPi.Infrastructure;
using BankingAPI.Interfaces.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Services.Domain;

public class WalletService : IWalletService
{
    private readonly ApplicationDbContext _context;

    public WalletService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task DepositFundsAsync(DepositFundsDto model)
    {
        var wallet = await GetByWalletNumberAsync(model.WalletNumber);
        
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            wallet.Amount = model.Amount + wallet.Amount > 0 ? wallet.Amount + model.Amount : throw new ApiException("You cannot withdraw more than you have");
            _context.UserWallets.Update(wallet);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<UserWalletEntity?> GetByIdAsync(Guid id)
    {
        return await _context.UserWallets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("Wallet was not found");
    }

    public async Task<UserWalletEntity?> GetByUserIdAsync(Guid id)
    {
        return await _context.UserWallets.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id)
               ?? throw new NotFoundException("Wallet was not found");
    }

    public async Task<UserWalletEntity?> GetByWalletNumberAsync(string number)
    {
         return await _context.UserWallets.AsNoTracking().FirstOrDefaultAsync(x => x.WalletNumber == number)
            ?? throw new NotFoundException("Wallet was not found");
    }

    public async Task TransferFundsAsync(TransferFundsDto model, string userId)
    {
        var userWallet = await GetByUserIdAsync(Guid.Parse(userId));
        
        var walletToTransfer = await GetByWalletNumberAsync(model.WalletNumber);
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var updateList = new List<UserWalletEntity>();
            userWallet.Amount = userWallet.Amount - model.Amount < 0 ? throw new ApiException("You can't transfer more than you have") : userWallet.Amount - model.Amount;
            walletToTransfer.Amount = userWallet.Amount + model.Amount;
            
            
            updateList.AddRange(userWallet, walletToTransfer);
            _context.UserWallets.UpdateRange(updateList);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new TransactionException(e.Message);
        }
        
        
    }
}