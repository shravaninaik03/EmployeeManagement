using Microsoft.AspNetCore.SignalR;

namespace Emp.DTOs;

public class EmployeeDashboardDto
{
    public int TotalEmployees{get; set;}
    public int ActiveEmployees{get; set;}

    public int InactiveEmployees{get; set; }

    public decimal AvgSalary{get; set; }

    public int TotaldDepartments{get; set;}

    public int Joinedthismonth{get; set;}

}