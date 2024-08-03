using AutoMapper;
using OrganizationManagement.Models;

namespace OrganizationManagement.Services;

public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDetails>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position));
    }
}
