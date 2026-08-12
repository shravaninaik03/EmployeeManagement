
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Emp.Interfaces;

namespace Emp.Modals;

public class User : IEntity
{
    public int UserId {get; set;}

    [Required]
    public string Username { get; set;} = string.Empty;

    [Required]
    public string PasswordHash { get; set;}=string.Empty;

    [Required]
    public string Role {get; set; } = string.Empty;

}