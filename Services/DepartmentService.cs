using Emp.Modals;
using Emp.Interfaces;
using System.Net.WebSockets;

namespace Emp.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _repository;

    public DepartmentService(IRepository<Department> repository)
    {
        _repository = repository;
    }
    
     public async Task <IEnumerable<Department>> GetDepartments()
    {
        return await _repository.GetAllAsyc();
    }

    public async Task <Department> AddDepartment(Department department)
    {
        await _repository.AddAsync(department);
        await _repository.SaveAsync();

        return department;
    }

    public async Task <Department?> UpdateDepartment(int id, Department department)
    {
        var existingdep = await _repository.GetByIdAsync(id);

        if(existingdep==null)
        return null;

        existingdep.DepartmentName = department.DepartmentName;

        await _repository.Update(existingdep);
        await _repository.SaveAsync();

        return existingdep;
    }
    public async Task <bool> Delete(int id)
    {
        var exi= await _repository.GetByIdAsync(id);
        if(exi==null)
        return false;

        await _repository.Delete(exi);
        await _repository.SaveAsync();
        return true;
    }

}
