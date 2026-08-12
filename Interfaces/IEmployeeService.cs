using Emp.Modals;
using Emp.DTOs;
using System.Runtime.CompilerServices;
namespace Emp.Interfaces;

public interface IEmployeeService
{
    Task <IEnumerable<Employee>> GetEmployees();

    Task <IEnumerable<Employee>> GetActiveEmployees();

    Task <IEnumerable<Employee>> GetEmployeesbySalary(bool ascending);

    Task <IEnumerable<Employee>> GetEmployeesbyPage(int page, int pagesize);

    Task <object> GetEmployeesbygroup(string groupby);

    Task <IEnumerable<EmpDto>> GetEmployeeDtos();

    Task <IEnumerable<Employee>> GetEmployeebySearch(string? fname,int? deptid, string? lname);
    Task <Employee> AddEmployee(CreateEmployeeDto employee);

    Task <Employee?> UpdateEmployee (int id, Employee employee );

    Task <bool> Delete(int id);
}