using System.Runtime.CompilerServices;

namespace Emp.DTOs;

public class EmpDto
{

    public int EmployeeId{ get; set; }
    public string EmpFname { get; set; } = string.Empty;

    public string EmpLname{ get; set; } = string.Empty;

    public string Empcode { get; set; }= string.Empty;
     public decimal Salary { get; set; }
     public DateTime JoiningDate { get; set; }

}


