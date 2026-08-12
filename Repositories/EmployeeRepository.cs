using Emp.Interfaces;
using Emp.Data;
using Emp.DTOs;
using Microsoft.EntityFrameworkCore;
namespace Emp.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    
    public EmployeeRepository(ApplicationDbContext context)
    {
        _context=context;
    }
public async Task<EmployeeDashboardDto?> GetEmployeeDashboardAsync()
{
    using var command = _context.Database.GetDbConnection().CreateCommand();

    command.CommandText = "GetEmployeeDashboard";
    command.CommandType = System.Data.CommandType.StoredProcedure;

    await _context.Database.OpenConnectionAsync();

    using var reader = await command.ExecuteReaderAsync();

    if (await reader.ReadAsync())
    {
        return new EmployeeDashboardDto
        {
            TotalEmployees = reader.GetInt32(reader.GetOrdinal("TotalEmployees")),
            ActiveEmployees = reader.GetInt32(reader.GetOrdinal("ActiveEmployees")),
            InactiveEmployees = reader.GetInt32(reader.GetOrdinal("InactiveEmployees")),
            AvgSalary = reader.GetDecimal(reader.GetOrdinal("AvgSalary")),
             TotaldDepartments= reader.GetInt32(reader.GetOrdinal("TotalDepartments")),
            Joinedthismonth = reader.GetInt32(reader.GetOrdinal("Joinedthismonth"))
        };
    }

    return null;
}

}