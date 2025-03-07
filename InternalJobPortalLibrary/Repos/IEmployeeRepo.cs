using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;

namespace InternalJobPortalLibrary.Repos
{
    public interface IEmployeeRepoAsync
    {
        Task InsertEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(string EmployeeID);
       Task UpdateEmployeeAsync(string EmployeeId,Employee employee);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(string employeeID);
        Task<Employee> GetEmployeeByEmailAsync(string email);
        Task<List<Employee>> GetEmployeeByJobIDAsync(string JobID);
      
    }
}
