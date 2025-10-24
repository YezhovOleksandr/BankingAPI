using BankingAPI.Common.Models.Identity;
using BankingAPI.Interfaces.Api;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers;

[Route("api/v1/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountApiService _accountApiService;

    public AccountController(IAccountApiService accountApiService)
    {
        _accountApiService = accountApiService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
    {
        await _accountApiService.RegisterAsync(registerDto);
        
        return Ok();
    }

    [HttpPost("connect/token")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        return Ok(await _accountApiService.LoginAsync(loginDto));
    }
}