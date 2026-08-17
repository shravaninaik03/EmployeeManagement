using Emp.Interfaces;
using Emp.Modals;
using Emp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Emp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var emp = await _employeeService.GetEmployees();
        return Ok(emp);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveEmployees()
    {
        var emp = await _employeeService.GetActiveEmployees();
        return Ok(emp);
    }

    [HttpGet("Salary")]
    public async Task<IActionResult> GetEmployeesbySalary(bool ascending)
    {
        var emp = await _employeeService.GetEmployeesbySalary(ascending);
        return Ok(emp);
    }

    [HttpGet("Page")]
    public async Task<IActionResult> GetEmployeesbyPage(int page, int pagesize)
    {
        var emp= await _employeeService.GetEmployeesbyPage(page, pagesize);
        return Ok(emp);
    }

    [HttpGet("Groupby")]

    public async Task<IActionResult> GetEmployeesbygroup(string groupby)
    {
        var emp= await _employeeService.GetEmployeesbygroup(groupby);
        return Ok(emp);
    }

    [HttpGet("Projection")]

    public async  Task <IActionResult> GetEmployeeDtos()
    {
        var emp = await _employeeService.GetEmployeeDtos();
        return Ok(emp);
    }

    [HttpGet("Search")]
    public async Task <IActionResult> GetEmployeebySearch(string? fname,int? deptid, string? lname)
    {
        var emp = await _employeeService.GetEmployeebySearch(fname, deptid, lname );
        if(!emp.Any())
        {
           return NotFound("Employee not found");
        }
        return Ok(emp);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddEmployee(CreateEmployeeDto employee)
    {
        var createdEmp = await _employeeService.AddEmployee(employee);
        return Ok(createdEmp);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
    {
        var updatedEmp = await _employeeService.UpdateEmployee(id, employeeDto);

        if (updatedEmp == null)
            return NotFound();

        return Ok(updatedEmp);
    }
    [Authorize(Roles ="Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deleted = await _employeeService.Delete(id);

        if (!deleted)
            return NotFound();

        return Ok("Employee deleted successfully");
    }
}