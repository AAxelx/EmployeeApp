using EmployeeApp.Common.Enums;
using EmployeeApp.Common.Models;
using EmployeeApp.DAL.Repositories.Abstractions;
using EmployeeApp.SAL.Services.Abstractions;

namespace EmployeeApp.SAL.Services
{
	public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repository;

		public EmployeesService(IEmployeesRepository repository)
		{
            _repository = repository;
		}

        public async Task<ServiceValueResult<Employee>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return new ServiceValueResult<Employee>(ResponseType.NotFound);
            }

            return new ServiceValueResult<Employee>(employee);
        }

        public async Task<ServiceValueResult<Employee>> UpdateEmployeeAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return new ServiceValueResult<Employee>(ResponseType.NotFound);
            }

            var isSuccess = await _repository.ToggleEmployeeEnableAsync(id);

            if (!isSuccess)
            {
                return new ServiceValueResult<Employee>(ResponseType.BadRequest);
            }

            employee.Enable = !employee.Enable;
            return new ServiceValueResult<Employee>(employee);
        }
    }
}

