using EmployeeApp.Common.Models;

namespace EmployeeApp.DAL.Repositories.Abstractions
{
	public interface IEmployeesRepository
	{
        Task<Employee> GetByIdAsync(int employeeId);
        Task<bool> ToggleEmployeeEnableAsync(int employeeId);
    }
}

