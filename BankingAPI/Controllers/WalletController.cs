using BankingAPI.Common.Models.Wallet;
using BankingAPI.Interfaces.Api;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers;

[Route("api/v1/wallets")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletApiService _walletApiService;

    public WalletController(IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var wallet = await _walletApiService.GetByIdAsync(id);
        
        return Ok(wallet);
    }

    [HttpPatch("{walletNumber}/transfer")]
    public async Task<IActionResult> TranferFunds(TransferFundsDto model, string walletNumber)
    {
        await _walletApiService.TransferMoneyAsync(model, walletNumber);
        
        return Ok();
    }

    [HttpPatch("{walletNumber}/funds")]
    public async Task<IActionResult> DepositFunds(DepositFundsDto model, string walletNumber)
    {
        await _walletApiService.DepositMoneyAsync(model, walletNumber);
        
        return Ok();
    }
}