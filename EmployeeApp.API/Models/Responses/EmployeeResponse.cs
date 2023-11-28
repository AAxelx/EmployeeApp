using EmployeeApp.Common.Models;

namespace EmployeeApp.API.Models.Responses
{
	public class EmployeeResponse
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }
        public bool Enable { get; set; }
        public List<Employee> Subordinates { get; set; } = new List<Employee>();
    }
}

