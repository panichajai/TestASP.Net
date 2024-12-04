using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeesService(ApplicationDbContext dbContext) 
        { 
            this._dbContext = dbContext;
        }
        public ResultModel AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var result = new ResultModel
            {
                status = 200,
                success = true,
            };
            try
            {
                var existingEmployee = _dbContext.employees.FirstOrDefault(e => e.Email == addEmployeeDto.Email);
                if (existingEmployee != null)
                {
                    result.success = false;
                    result.message = "An employee with this email already exists." + existingEmployee.Name;
                    result.status = 501;
                    return result;
                }
                if (string.IsNullOrWhiteSpace(addEmployeeDto.Name))
                {
                    result.success = false;
                    result.message = "Employee name cannot be empty." ;
                    result.status = 501;
                    return result;
                }
                var employeeEntity = new Employee()
                {
                    Name = addEmployeeDto.Name,
                    Email = addEmployeeDto.Email,
                    Phone = addEmployeeDto.Phone,
                    Salary = addEmployeeDto.Salary
                };

                _dbContext.employees.Add(employeeEntity);
                _dbContext.SaveChanges();
                result.data = employeeEntity;
            }
            catch (Exception ex) {
                return new ResultModel
                {
                    status = 500,
                    success = false,
                    message = ex.Message
                };
            }
            return result;
        }

        public ResultModel DeleteEmployee(int id)
        {
            var result = new ResultModel
            {
                status = 200,
                success = true,
            };
            try
            {

            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    status = 500,
                    success = false,
                    message = ex.Message
                };
            }
            return result;
        }

        public ResultModel GetEmployeeById(int id)
        {
            var result = new ResultModel
            {
                status = 200,
                success = true,
            };
            try
            {
                var employee = _dbContext.employees.Find(id);
                if (employee is null)
                {
                    result.success = false;
                    result.message = "Not Have Employee ById : " + id ;
                    result.status = 501;
                    return result;
                }
                result.data = employee;
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    status = 500,
                    success = false,
                    message = ex.Message
                };
            }
            return result;
        }

        public ResultModel GetEmployees()
        {
            var result = new ResultModel
            {
                status = 200,
                success = true,
            };
            try
            {
               var employees = _dbContext.employees.Select(s=> new EmployeeModel
               {
                   Id= s.Id,
                   Email= s.Email,
                   Name= s.Name,
                   Salary= s.Salary,
                   Phone= s.Phone,
                   Amount = 10 + s.Id
               }).ToList();
                result.data= employees;
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    status = 500,
                    success = false,
                    message = ex.Message
                };
            }
            return result;
        }

        public ResultModel UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var result = new ResultModel
            {
                status = 200,
                success = true,
            };
            try
            {
                var employee = _dbContext.employees.Find(id);

                if (employee is null)
                {
                    result.success = false;
                    result.message = "Not Have Employee ById : " + id;
                    result.status = 501;
                    return result;
                }

                employee.Name = updateEmployeeDto.Name;
                employee.Email = updateEmployeeDto.Email;
                employee.Phone = updateEmployeeDto.Phone;
                employee.Salary = updateEmployeeDto.Salary;

                _dbContext.SaveChanges();
                result.data= employee;
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    status = 500,
                    success = false,
                    message = ex.Message
                };
            }
            return result;
        }
    }
}
