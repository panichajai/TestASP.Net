using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }
       public DbSet<Employee> employees { get; set; } //กำหนดให้ตาราง employees ในฐานข้อมูล คลาส Employee.cs (ใน Entity)
       public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
