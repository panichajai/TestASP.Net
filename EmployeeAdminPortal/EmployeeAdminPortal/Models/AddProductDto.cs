﻿namespace EmployeeAdminPortal.Models
{
    public class AddProductDto
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }
}