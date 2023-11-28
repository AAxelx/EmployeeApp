using System;
using EmployeeApp.Common.Entities;

namespace EmployeeApp.Common.Models
{
	public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }
        public bool Enable { get; set; }
        public List<Employee> Subordinates { get; set; } = new List<Employee>();
    }
}

