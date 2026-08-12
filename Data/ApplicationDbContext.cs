using Microsoft.EntityFrameworkCore;
using Emp.Modals;
namespace Emp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Department> Departments {get; set;}
    public DbSet<User> Users { get; set; }
}