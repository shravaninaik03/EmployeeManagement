using Emp.Interfaces;

namespace Emp.Modals;

public class Department : IEntity
{
    public int DepartmentId{ get; set; }

    public string DepartmentName{ get; set; } = string.Empty;

    public ICollection<Employee> Employees {get; set; } = new List<Employee>();
}