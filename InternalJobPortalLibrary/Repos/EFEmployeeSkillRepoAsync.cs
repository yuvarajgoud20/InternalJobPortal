using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace InternalJobPortalLibrary.Repos
{
    public class EFEmployeeSkillRepoAsync : IEmployeeSkillRepoAsync
    {
        InternalJobPortalDBContext ctx = new InternalJobPortalDBContext();
        public async Task DeleteEmployeeSkillAsync(string EmployeeID, string SkillID)
        {
            try
            {
                EmployeeSkill es2del = await GetEmployeeSkillAsync(EmployeeID, SkillID);
                ctx.EmployeeSkills.Remove(es2del);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }

        public async Task<List<EmployeeSkill>> GetAllEmployeeSkillsAsync()
        {
            try
            {
                List<EmployeeSkill> empSkills = await ctx.EmployeeSkills.ToListAsync();
                return empSkills;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }

        public async Task<EmployeeSkill> GetEmployeeSkillAsync(string EmployeeID, string SkillID)
        {
            try
            {
                EmployeeSkill employeeSkill = await (from empSkill in ctx.EmployeeSkills where empSkill.EmployeeID == EmployeeID && empSkill.SkillID == SkillID select empSkill).FirstAsync();
                return employeeSkill;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }

        public async Task<List<EmployeeSkill>> GetEmployeeSkillsByEmployeeIDAsync(string EmployeeID)
        {
            try
            {
                List<EmployeeSkill> esByEmpID = await (from empSkill in ctx.EmployeeSkills where empSkill.EmployeeID == EmployeeID select empSkill).ToListAsync();
                return esByEmpID;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }

        public async Task<List<EmployeeSkill>> GetEmployeeSkillsBySkillIDAsync(string skillID)
        {
            try
            {
                List<EmployeeSkill> emp = await (from e in ctx.EmployeeSkills where e.SkillID == skillID select e).ToListAsync();
                return emp;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }

        public async Task AddEmployeeSkillAsync(EmployeeSkill employeeSkill)
        {
            try
            {
                await ctx.EmployeeSkills.AddAsync(employeeSkill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task UpdateEmployeeSkillAsync(string EmployeeID, string SkillID, EmployeeSkill employeeSkill)
        {
            try
            {
                EmployeeSkill emp2edit = await GetEmployeeSkillAsync(EmployeeID, SkillID);
                emp2edit.SkillExperience = employeeSkill.SkillExperience;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }
    }
}
