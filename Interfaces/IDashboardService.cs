using Emp.DTOs;

namespace Emp.Interfaces;

public interface IDashboardService
{
    Task<EmployeeDashboardDto?> GetEmployeeDashboard();
}