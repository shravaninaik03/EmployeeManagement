using Emp.Interfaces;

namespace Emp.Modals;

public class Employee : IEntity
{
    public int EmployeeId{ get; set; }

    public string Empcode { get; set; }= string.Empty;

    public string EmpFname { get; set; } = string.Empty;

    public string EmpLname { get; set; } = string.Empty;

    public string Empemail { get; set; } = string.Empty;

     public int Empmobile { get; set; }
    
    public DateOnly DOB { get; set; }

    public int DepartmentId { get; set; }

    public decimal Salary { get; set; }

    public DateOnly JoiningDate { get; set; }

    public bool IsActive { get; set; }

    public Department? Department { get; set; }
}