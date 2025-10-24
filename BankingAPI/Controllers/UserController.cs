using BankingAPI.Interfaces.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers;

[Route("api/v1/users")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    private readonly IUserApiService _userApiService;

    public UserController(IUserApiService userApiService)
    {
        _userApiService = userApiService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userApiService.GetUsers();
        
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userApiService.GetUserById(id);
        
        return Ok(user);
    }

    [HttpGet("wallet/{walletNumber}")]
    public async Task<IActionResult> GetUserByWalletId(string walletNumber)
    {
        var user = await _userApiService.GetUserByWalletNumber(walletNumber);
        
        return Ok(user);
    }
}