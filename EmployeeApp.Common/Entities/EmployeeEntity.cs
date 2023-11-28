﻿using System;
namespace EmployeeApp.Common.Entities
{
	public class EmployeeEntity
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }
        public bool Enable { get; set; }
    }
}

