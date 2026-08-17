using Emp.DTOs;
using Emp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Emp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
            _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var token = await _authService.Login(login);

        if(token == null)
        {
           return Unauthorized("Invalid username and password");
        }
         return Ok(new { token });
    }

}