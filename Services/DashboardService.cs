using Emp.DTOs;
using Emp.Interfaces;

namespace Emp.Services;

public class DashboardService: IDashboardService
{
    private readonly IEmployeeRepository _emprepository;

    public DashboardService(IEmployeeRepository emprepository)
    {
        _emprepository= emprepository;
    }

    public async Task<EmployeeDashboardDto?> GetEmployeeDashboard()
    {
       return await _emprepository.GetEmployeeDashboardAsync();
    }
}