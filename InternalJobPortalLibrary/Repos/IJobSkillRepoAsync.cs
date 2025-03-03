using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;

namespace InternalJobPortalLibrary.Repos
{
    public interface IJobSkillRepoAsync
    {
        Task<List<JobSkill>> GetAllJobSkillsAsync();
        Task<JobSkill> GetJobSkillAsync(string JobID, string SkillID);
        Task<List<JobSkill>> GetJobSkillsByJobIDAsync(string jobID);
        Task<List<JobSkill>> GetJobSkillsBySkillIDAsync(string skillID);
        Task AddJobSkillAsync(JobSkill jobSkill);
        Task UpdateJobSkillAsync(string JobID, string SkillID, JobSkill jobSkill);
        Task DeleteJobSkillAsync(string JobID, string SkillID);
    }
}
