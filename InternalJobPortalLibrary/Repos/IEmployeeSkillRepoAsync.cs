using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;

namespace InternalJobPortalLibrary.Repos
{
    public  interface IEmployeeSkillRepoAsync
    {
        Task AddEmployeeSkillAsync(EmployeeSkill employeeSkill);
        Task DeleteEmployeeSkillAsync(string EmployeeID, string SkillID);
        Task UpdateEmployeeSkillAsync(string EmployeeID, string SkillID, EmployeeSkill employeeSkill);
        Task<List<EmployeeSkill>> GetAllEmployeeSkillsAsync();
        Task<EmployeeSkill> GetEmployeeSkillAsync(string EmployeeID, string SkillID);
        Task<List<EmployeeSkill>> GetEmployeeSkillsByEmployeeIDAsync(string EmployeeID);
        Task<List<EmployeeSkill>> GetEmployeeSkillsBySkillIDAsync(string skillID);
    }
}
