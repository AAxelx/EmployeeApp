using EmployeeApp.Common.Models;

namespace EmployeeApp.SAL.Services.Abstractions
{
	public interface IEmployeesService
	{
        Task<ServiceValueResult<Employee>> GetEmployeeByIdAsync(int id);
        Task<ServiceValueResult<Employee>> UpdateEmployeeAsync(int id);
    }
}

