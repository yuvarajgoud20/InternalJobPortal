using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace InternalJobPortalLibrary.Repos
{
    public class EFJobRepoAsync : IJobRepoAsync
    {
        InternalJobPortalDBContext ctx = new InternalJobPortalDBContext(); //Creating object for DBContext
        public async Task DeleteJob(string jid)
        {
            try
            {
                Job jdel = await GetJob(jid); //getting job
                ctx.Jobs.Remove(jdel); //removing job from jobs list
                ctx.SaveChanges(); //saving changes
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message); //Exception handling
            }
        }

        public async Task<List<Job>> GetAllJobs()
        {
            try
            {
                List<Job> jobs = await ctx.Jobs.ToListAsync(); //getting all jobs from jobs list
                return jobs; //returning jobs
            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in getting all jobs"); //Exception handling
            }
        }

        public async Task<Job> GetJob(string jid)
        {
            try
            {
                Job job = await (from c in ctx.Jobs where c.JobID == jid select c).FirstAsync(); //getting job from jobs list
                return job;
            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in getting job"); //Exception handling
            }
        }

        public async Task InsertJob(Job job)
        {
            try
            {
                await ctx.Jobs.AddAsync(job); //adding job to jobs list
                await ctx.SaveChangesAsync();
            }
            catch (InternalJobPortalException ex)
            {
                throw new InternalJobPortalException("hi"); //Exception handling
            }
        }

        public async Task UpdateJob(string jid, Job job)
        {
            try
            {
                Job jedit = await GetJob(jid);
                jedit.JobTitle = job.JobTitle; //updating job title
                jedit.JobDescription = job.JobDescription; //updating job description
                jedit.Salary = job.Salary; //updating salary
                ctx.SaveChanges(); //saving changes
            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in updating job"); //Exception handling
            }
        }

    }
}
