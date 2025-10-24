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
}