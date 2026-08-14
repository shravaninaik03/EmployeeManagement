using Emp.Modals;
using Emp.Interfaces;
using System.Net.WebSockets;
using Emp.DTOs;
using AutoMapper;
using Emp.Mappings;

namespace Emp.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _repository;
    private readonly ILogger<EmployeeService> _logger;

    private readonly IMapper _mapper;  // IMapper provided by AutoMapper
    public EmployeeService(IRepository<Employee> repository, ILogger<EmployeeService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
     public async Task <IEnumerable<Employee>> GetEmployees()
    {
        try{
        return await _repository.GetAllAsyc();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, " Failed to get employess");
            throw;
        }
    }

    public async Task <IEnumerable<Employee>> GetActiveEmployees()
    {
        var emp = await _repository.GetAllAsyc();
        return emp.Where(e=> e.IsActive);
    }

    public async Task <IEnumerable<Employee>> GetEmployeesbySalary(bool ascending)
    {
        var emp = await _repository.GetAllAsyc();

        if(ascending)
        {
            return emp.OrderBy(e=> e.Salary);
        }
        return emp.OrderByDescending(e=> e.Salary);
    }
    public async Task <IEnumerable<Employee>> GetEmployeesbyPage(int page, int pagesize)
    {
        var emp = await _repository.GetAllAsyc();

        return emp.Skip((page-1) * pagesize).Take(pagesize);
    }

    public async Task <object> GetEmployeesbygroup(string groupby) //common return type 
    {
        var emp = await _repository.GetAllAsyc();

        if(groupby == "department")
        {
            return emp.GroupBy(e=> e.DepartmentId);
        }

        if(groupby =="joiningdate")
        {
            return emp.GroupBy(e=> e.JoiningDate.Year );
        }
        if(groupby =="salary")
        {
            return emp.GroupBy(e=> e.Salary );
        }

        return new { Message = "Invalid"};
    }

    public async Task<IEnumerable<EmpDto>> GetEmployeeDtos()
    {
        var emp = await _repository.GetAllAsyc();

        return emp.Select(e=> new EmpDto
        {
            EmpFname= e.EmpFname,
            EmpLname= e.EmpLname,
            Empcode= e.Empcode,
            Salary= e.Salary
        });
    }
    public async Task <Employee> AddEmployee(CreateEmployeeDto employee)
    {
        try
    {
        var employeeEntity = new Employee
        {
            Empcode = employee.Empcode,
            EmpFname = employee.EmpFname,
            EmpLname = employee.EmpLname,
            Empemail = employee.Empemail,
            Empmobile = employee.Empmobile,
            DOB = employee.DOB,
            DepartmentId = employee.DepartmentId,
            Salary = employee.Salary,
            JoiningDate = employee.JoiningDate,
            IsActive = employee.IsActive
        };

        await _repository.AddAsync(employeeEntity);
        await _repository.SaveAsync();

        return employeeEntity;
    }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee creation failed");
            throw;
        }
    }
     
     public async  Task <IEnumerable<Employee>> GetEmployeebySearch(string? fname,int? deptid, string? lname)
    {
        var emp = await _repository.GetAllAsyc();

        if(!string.IsNullOrEmpty(fname))
        {
            emp = emp.Where(e=> e.EmpFname.Contains(fname));
        }
        if (deptid.HasValue)
        {
            emp = emp.Where(e=> e.DepartmentId == deptid.Value);
        }
        if (!string.IsNullOrEmpty(lname))
        {
            emp = emp.Where(e=> e.EmpLname.Contains(lname));
        }
        return emp;
     }

    public async Task <Employee?> UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
    {
        try{
        var existingEmployee = await _repository.GetByIdAsync(id);

        if(existingEmployee==null)
        return null;

        _mapper.Map(employeeDto , existingEmployee);

        await _repository.Update(existingEmployee);
        await _repository.SaveAsync();

        return existingEmployee;
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Update failed");
            throw;
        }
    }
    public async Task <bool> Delete(int id)
    {
        try{
        var exi= await _repository.GetByIdAsync(id);
        if(exi==null)
        return false;

        await _repository.Delete(exi);
        await _repository.SaveAsync();
        return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Deletion failed");
            throw;
        }
    }

}
