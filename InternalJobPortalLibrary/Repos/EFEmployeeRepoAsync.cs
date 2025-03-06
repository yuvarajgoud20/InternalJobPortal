using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary.Repos;
using Microsoft.EntityFrameworkCore;

namespace InternalJobPortalLibrary.Repos
{
    public class EFEmployeeRepoAsync : IEmployeeRepoAsync
    {
        InternalJobPortalDBContext ctx=new InternalJobPortalDBContext();
        public async Task DeleteEmployeeAsync(string EmployeeID)
        {
            try
            {

                Employee empdel = await GetEmployeeAsync(EmployeeID);
                ctx.Employees.Remove(empdel);
                ctx.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await ctx.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeAsync(string EmployeeID)
        {
            try
            {
                Employee employee = await (from emp in ctx.Employees where emp.EmployeeID == EmployeeID select emp).FirstAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException("No Such Employee Id");
            }

        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            try
            {
                await ctx.Employees.AddAsync(employee);
                await ctx.SaveChangesAsync();
            }
           catch(Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }

        }

        public async Task UpdateEmployeeAsync(string EmployeeID, Employee employee)
        {
            Employee empedit=await GetEmployeeAsync(EmployeeID);
            empedit.EmployeeName = employee.EmployeeName;
            //empedit.EmployeeSkills = employee.EmployeeSkills;
            empedit.EmailID = employee.EmailID;
            empedit.PhoneNo = employee.PhoneNo;
            empedit.TotalExperience = employee.TotalExperience;
            empedit.JobID = employee.JobID;
            await ctx.SaveChangesAsync();
        }
       
       public async Task<List<Employee>> GetEmployeeByJobIDAsync(string JobID)
        {
            List<Employee> employees = await(from j in ctx.Employees where j.JobID == JobID select j).ToListAsync();
            return employees;
        }
    }
}
