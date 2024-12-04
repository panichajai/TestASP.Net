using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CustomersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var allCustomers = dbContext.customers.ToList();
            return Ok(allCustomers);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = dbContext.customers.Find(id);
            if(customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public IActionResult AddCustomer(AddCustomerDto addCustomerDto)
        {
            var existingCustomer = dbContext.customers.FirstOrDefault(e => e.Email == addCustomerDto.Email);

            if (existingCustomer != null)
            {
                return Conflict(new { message = "An customer with this email already exists." });
            }

            if (string.IsNullOrWhiteSpace(addCustomerDto.Name))
            {
                return BadRequest(new { message = "Customer name cannot be empty." });
            }

            var customerEntity = new Customer()
            {
                Name = addCustomerDto.Name,
                Email = addCustomerDto.Email,
                Phone = addCustomerDto.Phone
            };

            dbContext.customers.Add(customerEntity);
            dbContext.SaveChanges();
            return Ok(customerEntity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateCustomer(int id, UpdateCustomerDto updatecustomerDto)
        {
            var customer = dbContext.customers.Find(id);

            if (customer is null)
            {
                return NotFound();
            }

            customer.Name = updatecustomerDto.Name;
            customer.Email = updatecustomerDto.Email;
            customer.Phone = updatecustomerDto.Phone;

            dbContext.SaveChanges();

            return Ok(customer);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = dbContext.customers.Find(id);

            if (customer is null)
            {
                return NotFound();
            }

            dbContext.customers.Remove(customer);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
