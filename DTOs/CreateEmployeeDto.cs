using System.ComponentModel.DataAnnotations;

namespace Emp.DTOs;

public class CreateEmployeeDto
{
    [Required]
    public string Empcode { get; set; } = string.Empty;

    [Required]
    public string EmpFname { get; set; } = string.Empty;

    [Required]
    public string EmpLname { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Empemail { get; set; } = string.Empty;

    [Required]
    public int Empmobile { get; set; } 
    public DateOnly DOB { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "DepartmentId must be greater than 0.")]
    public int DepartmentId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
    public decimal Salary { get; set; }

    public DateOnly JoiningDate { get; set; }

    public bool IsActive { get; set; }
}