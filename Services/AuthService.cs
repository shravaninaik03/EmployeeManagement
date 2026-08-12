using Emp.Data;
using Emp.DTOs;
using Emp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Emp.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string?> Login(LoginDto login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u=> u.Username == login.Username);

        if(user==null)
        {
                    return null;

        }
        return null;
    } 
}