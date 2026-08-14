using AutoMapper;
using Emp.DTOs;
using Emp.Modals;

namespace Emp.Mappings;

public class EmployeeUpdateProfile : Profile
{
    public EmployeeUpdateProfile()
    {
        CreateMap<EmployeeUpdateDto , Employee>();
    }
}