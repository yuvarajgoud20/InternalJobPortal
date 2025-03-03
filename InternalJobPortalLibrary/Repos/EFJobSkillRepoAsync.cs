using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary;
using InternalJobPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace InternalJobPortalLibrary.Repos
{
    public class EFJobSkillRepoAsync : IJobSkillRepoAsync
    {
        InternalJobPortalDBContext ctx = new InternalJobPortalDBContext();

        public async Task AddJobSkillAsync(JobSkill jobSkill)
        {
            try
            {
                await ctx.JobSkills.AddAsync(jobSkill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task DeleteJobSkillAsync(string JobID, string SkillID)
        {
            try
            {
                JobSkill js2del = await GetJobSkillAsync(JobID, SkillID);
                ctx.JobSkills.Remove(js2del);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task<List<JobSkill>> GetAllJobSkillsAsync()
        {
            try
            {
                List<JobSkill> allJobSkills = await ctx.JobSkills.ToListAsync();
                return allJobSkills;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task<JobSkill> GetJobSkillAsync(string JobID, string SkillID)
        {
            try
            {
                JobSkill jobSkill = await (from job in ctx.JobSkills where job.JobID == JobID && job.SkillID == SkillID select job).FirstAsync();
                return jobSkill;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task<List<JobSkill>> GetJobSkillsByJobIDAsync(string jobID)
        {
            try
            {
                List<JobSkill> jsByjobID = await (from jobSkills in ctx.JobSkills where jobSkills.JobID == jobID select jobSkills).ToListAsync();
                return jsByjobID;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task<List<JobSkill>> GetJobSkillsBySkillIDAsync(string skillID)
        {
            try
            {
                List<JobSkill> jsByjobID = await(from jobSkills in ctx.JobSkills where jobSkills.SkillID == skillID select jobSkills).ToListAsync();
                return jsByjobID;
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task UpdateJobSkillAsync(string JobID, string SkillID, JobSkill jobSkill)
        {
            try
            {
                JobSkill js2edit = await GetJobSkillAsync(JobID, SkillID);
                js2edit.Experience = jobSkill.Experience;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }
    }
}
