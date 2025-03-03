using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;

namespace InternalJobPortalLibrary.Repos
{
    public interface IJobRepoAsync
    {

        Task<List<Job>> GetAllJobs();
        Task<Job> GetJob(string jid);
        Task InsertJob(Job job);
        Task UpdateJob(string jid, Job job);
        Task DeleteJob(string jid);
    }
}
