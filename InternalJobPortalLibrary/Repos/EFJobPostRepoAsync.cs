using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPostLibrary.Models;
using Microsoft.EntityFrameworkCore;


namespace JobPostLibrary.Repos
{
    public class EFJobPostRepoAsync : IJobPostRepoAsync
    {
        InternalJobPortalDBContext ctx = new InternalJobPortalDBContext();

       

      
           
            public async Task AddJobPostAsync(JobPost jobPost)
            {
                await ctx.JobPosts.AddAsync(jobPost);
                await ctx.SaveChangesAsync();
            }

            public async Task DeleteJobPostAsync(int postId)
            {
                JobPost jp2d = await GetJobPostAsync(postId);
                ctx.JobPosts.Remove(jp2d);
                await ctx.SaveChangesAsync();
            }

            public async Task<List<JobPost>> GetAllJobPostsAsync()
            {
                return await ctx.JobPosts.ToListAsync();
            }

            public async Task<JobPost> GetJobPostAsync(int postId)
            {
                JobPost jp = await ctx.JobPosts.FindAsync(postId);
                if (jp==null)
                {
                    throw new InternalJobPortalException("Invalid Job Post ID");
                }
                return jp;
            }

            public async Task<List<JobPost>> GetJobPostsByJobIdAsync(string jobId)
            {
                return await ctx.JobPosts.Where(jp => jp.JobID == jobId).ToListAsync();
            }

            public async Task UpdateJobPostAsync(int postId, JobPost jobPost)
            {
                JobPost jp2edit = await GetJobPostAsync(postId);
                jp2edit.DateOfPosting = jobPost.DateOfPosting;
                jp2edit.LastDateToApply = jobPost.LastDateToApply;
                jp2edit.NoOfVacancies = jobPost.NoOfVacancies;

                await ctx.SaveChangesAsync();
            }
        }
    }


