using Emp.DTOs;

namespace Emp.Interfaces;

public interface IEmployeeRepository
{
    Task <EmployeeDashboardDto?> GetEmployeeDashboardAsync();   
}