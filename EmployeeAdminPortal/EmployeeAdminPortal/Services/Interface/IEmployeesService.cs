using EmployeeAdminPortal.Models;

namespace EmployeeAdminPortal.Services.Interface
{
    public interface IEmployeesService
    {
        ResultModel GetEmployees();
        ResultModel GetEmployeeById(int id);
        ResultModel AddEmployee(AddEmployeeDto addEmployeeDto);
        ResultModel UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto);
        ResultModel DeleteEmployee(int id);
    }
}
