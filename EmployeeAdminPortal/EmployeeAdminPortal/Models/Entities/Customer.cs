namespace EmployeeAdminPortal.Models.Entities
{
    //กำหนดคอลัมน์ตาราง ในฐานข้อมูล SQL Server
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
    }
}
