using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPostLibrary.Repos;

using Microsoft.EntityFrameworkCore;
using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary;


namespace JobPostLibrary.Repos
{
    public class EFJobPostRepoAsync : IJobPostRepoAsync
    {
        InternalJobPortalDBContext ctx = new InternalJobPortalDBContext();

        public async Task AddJobPostAsync(JobPost jobPost)
        {
            try
            {
                await ctx.JobPosts.AddAsync(jobPost);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex){
                throw new InternalJobPortalException(ex.InnerException.Message);
            }
        }
      

        public async Task DeleteJobPostAsync(int postId)
            {
                try
                {
                    JobPost jp2d = await GetJobPostAsync(postId);
                    ctx.JobPosts.Remove(jp2d);
                    await ctx.SaveChangesAsync();
                }
                catch
                {
                    throw new InternalJobPortalException("No such PostId");
                }
            }

            public async Task<List<JobPost>> GetAllJobPostsAsync()
            {
                return await ctx.JobPosts.ToListAsync();
            }

            public async Task<JobPost> GetJobPostAsync(int postId)
            {
                try
                {
                    JobPost jp = await ctx.JobPosts.FindAsync(postId);
                    return jp;
                }
                catch
                {
                    throw new InternalJobPortalException("Invalid Job Post ID");
                }
            }

            public async Task<List<JobPost>> GetJobPostsByJobIdAsync(string jobId)
            {
                return await ctx.JobPosts.Where(jp => jp.JobID == jobId).ToListAsync();
            }

            public async Task UpdateJobPostAsync(int postId, JobPost jobPost)
            {
                try
                {
                    JobPost jp2edit = await GetJobPostAsync(postId);
                    jp2edit.DateOfPosting = jobPost.DateOfPosting;
                    jp2edit.LastDateToApply = jobPost.LastDateToApply;
                    jp2edit.NoOfVacancies = jobPost.NoOfVacancies;

                    await ctx.SaveChangesAsync();
                }
                catch
                {
                    throw new InternalJobPortalException("No such PostId");
                }
            }
        }
    }


