using System.Data.SqlClient;
using AutoMapper;
using EmployeeApp.Common.Entities;
using EmployeeApp.Common.Models;
using EmployeeApp.DAL.Repositories.Abstractions;
namespace EmployeeApp.DAL.Repositories
{
	public class EmployeesRepository : IEmployeesRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public EmployeesRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            var employee = new Employee();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                WITH EmployeeCTE AS (
                SELECT Id, Name, ManagerId, Enable
                FROM Employee
                WHERE Id = @EmployeeId
                
                UNION ALL
                
                SELECT e.Id, e.Name, e.ManagerId, e.Enable
                FROM Employee e
                JOIN EmployeeCTE ecte ON e.ManagerId = ecte.Id
                )
                SELECT Id, Name, ManagerId, Enable
                FROM EmployeeCTE";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var employees = new Dictionary<int, Employee>();

                        while (await reader.ReadAsync())
                        {
                            var currentEmployee = new EmployeeEntity
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                ManagerId = reader["ManagerId"] == DBNull.Value
                                ? null : (int?)reader["ManagerId"],
                                Enable = Convert.ToBoolean(reader["Enable"])
                            };

                            employees[currentEmployee.Id] = _mapper.Map<Employee>(currentEmployee);

                            if (!currentEmployee.ManagerId.HasValue)
                            {
                                employee = _mapper.Map<Employee>(currentEmployee); 
                            }
                            else
                            {
                                employees[currentEmployee.ManagerId.Value].Subordinates.Add(_mapper.Map<Employee>(currentEmployee));
                            }
                        }
                    }
                }
            }

            return employee;
        }

        public async Task<bool> ToggleEmployeeEnableAsync(int employeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Employee SET Enable = CASE WHEN Enable = 1 THEN 0 ELSE 1 END WHERE Id = @EmployeeId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    await connection.OpenAsync();
                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}

