using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Identity.Client;

namespace InternalJobPortalLibrary.Repos
{
    public class EFApplyJobRepo : IApplyJobRepo
    {
        InternalJobPortalDBContext ctx = new InternalJobPortalDBContext();
        public async Task DeleteApplyJob(int postId, string employeeID)
        {
            try
            {
                ApplyJob applyJob = await GetApplyJob(postId, employeeID);
                ctx.ApplyJobs.Remove(applyJob);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }

        public async Task<List<ApplyJob>> GetAllApplyJobs()
        {
            return await ctx.ApplyJobs.ToListAsync();
        }

        public async Task<ApplyJob> GetApplyJob(int postID, string employeeID)
        {
            try
            {
                ApplyJob applyJob = await (from aj in ctx.ApplyJobs where aj.PostID == postID && aj.EmployeeID == employeeID select aj).FirstAsync();
                return applyJob;
            }
            catch
            {
                throw new InternalJobPortalException("No such Application Found");
            }
        }

        public async Task<List<ApplyJob>> GetApplyJobsByEmployeeID(string employeeID)
        {
            List<ApplyJob> applyJobs = await (from aj in ctx.ApplyJobs where aj.EmployeeID == employeeID select aj).ToListAsync();
            return applyJobs;
        }

        public async Task<List<ApplyJob>> GetApplyJobsByPostID(int postID)
        {
            List<ApplyJob> applyJobs = await (from aj in ctx.ApplyJobs where aj.PostID == postID select aj).ToListAsync();
            return applyJobs;
        }

        public async Task InsertApplyJob(ApplyJob applyJob)
        {
            try
            {
                await ctx.ApplyJobs.AddAsync(applyJob);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }

        public async Task UpdateApplyJob(int postId, string employeeID, ApplyJob applyJob)
        {
            try
            {
                ApplyJob aplJob = await GetApplyJob(postId, employeeID);
                aplJob.ApplicationStatus = applyJob.ApplicationStatus;
                ctx.ApplyJobs.Update(aplJob);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalJobPortalException(ex.Message);
            }
        }
    }
}
