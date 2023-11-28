using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EmployeeApp.SAL.Services.Abstractions;
using EmployeeApp.Common.Models;
using EmployeeApp.API.Models.Responses;

namespace EmployeeApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/users/{userId}/[controller]")]
    public class EmployeesController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly IEmployeesService _service;

		public EmployeesController(IMapper mapper, IEmployeesService service)
		{
			_mapper = mapper;
			_service = service;
		}

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetByIdAsync(int employeeId)
        {
            var result = await _service.GetEmployeeByIdAsync(employeeId);

            return MapResponse(result, _mapper.Map<Employee, EmployeeResponse>);
        }



        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int employeeId)
        {
            var result = await _service.UpdateEmployeeAsync(employeeId);

            return MapResponse(result, _mapper.Map<Employee, EmployeeResponse>);
        }
    }
}

