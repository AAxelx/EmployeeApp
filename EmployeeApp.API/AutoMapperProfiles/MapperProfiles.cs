using EmployeeApp.Common.Entities;
using EmployeeApp.Common.Models;
using AutoMapper;
using EmployeeApp.API.Models.Responses;

namespace EmployeeApp.API.AutoMapperProfiles
{
	public class MapperProfiles : Profile
	{
		public MapperProfiles()
		{
            CreateMap<EmployeeEntity, Employee>();
            CreateMap<Employee, EmployeeResponse>();
        }
    }
}