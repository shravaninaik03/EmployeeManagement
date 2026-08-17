using Emp.Interfaces;
using Emp.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _deptService;

    public DepartmentController(IDepartmentService deptService)
    {
        _deptService = deptService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var dept = await _deptService.GetDepartments();
        return Ok(dept);
    }
    [HttpPost]
    public async Task<IActionResult> AddDepartment(Department department)
    {
        var createddept = await _deptService.AddDepartment(department);
        return Ok(createddept);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, Department department)
    {
        var updateddept = await _deptService.UpdateDepartment(id,  department);

        if (updateddept == null)
            return NotFound();

        return Ok(updateddept);
    }
    [Authorize(Roles ="Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var deleted = await _deptService.Delete(id);

        if (!deleted)
            return NotFound();

        return Ok("Department deleted successfully");
    }
}