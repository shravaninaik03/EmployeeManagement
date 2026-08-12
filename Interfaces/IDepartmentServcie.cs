using Emp.Modals;

namespace Emp.Interfaces;

public interface IDepartmentService
{
    Task <IEnumerable<Department>> GetDepartments();

    Task <Department> AddDepartment(Department department);

    Task <Department?> UpdateDepartment (int id, Department department );

    Task <bool> Delete(int id);
}