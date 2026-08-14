using Emp.DTOs;

namespace Emp.Interfaces;

public interface IAuthService
{
    Task<string?> Login(LoginDto login);
}