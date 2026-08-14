using Microsoft.EntityFrameworkCore;
using Emp.Modals;
namespace Emp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Department> Departments {get; set;}
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Employee>()
        .HasOne(e => e.Department)
        .WithMany(d => d.Employees)
        .HasForeignKey(e => e.DepartmentId)
        .OnDelete(DeleteBehavior.SetNull);
}

}
