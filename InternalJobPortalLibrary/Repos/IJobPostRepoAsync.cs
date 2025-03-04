using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPostLibrary.Models;

namespace JobPostLibrary.Repos
{
    public interface IJobPostRepoAsync
    {
       


       
            Task<List<JobPost>> GetAllJobPostsAsync();
            Task<JobPost> GetJobPostAsync(int postId);
            Task<List<JobPost>> GetJobPostsByJobIdAsync(string jobId);
            Task AddJobPostAsync(JobPost jobPost);
            Task UpdateJobPostAsync(int postId, JobPost jobPost);
            Task DeleteJobPostAsync(int postId);
        }
    }





