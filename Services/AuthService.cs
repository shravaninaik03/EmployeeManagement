using Emp.Data;
using Emp.DTOs;
using Emp.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BCrypt.Net;
using System.Security.Claims;
using Emp.Modals;
using System.Runtime.CompilerServices;

namespace Emp.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration= configuration;
    }

    public async Task<string?> Login(LoginDto login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u=> u.Username == login.Username);

        if(user==null)
        {
            return null;
        }

        bool passvalid = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);

        if(!passvalid)
        {
            return null;
        }
        var claims = new[]
        {
          new Claim(ClaimTypes.Name, user.Username),
          new Claim(ClaimTypes.Role, user.Role)
        };                                                  // It prepares the secret key so the backend 
                                                            // can later check whether the JWT is genuine.

        var key = new SymmetricSecurityKey(
         Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(               // actual jwt token
            claims: claims, expires: DateTime.UtcNow.AddHours(1),signingCredentials: credentials);
    
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;                                         //Convert jwt obj into token string to send to frontend
    } 
}
