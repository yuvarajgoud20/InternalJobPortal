using InternalJobPortalLibrary;
using InternalJobPortalLibrary.Models;
using JobPostLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPostWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class JobPostController : ControllerBase
    {
        private readonly IJobPostRepoAsync jobRepo;

        public JobPostController(IJobPostRepoAsync repository)
        {
            jobRepo = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<JobPost> jobPosts = await jobRepo.GetAllJobPostsAsync();
            return Ok(jobPosts);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult> GetOne(int postId)
        {
            try
            {
                JobPost jobPost = await jobRepo.GetJobPostAsync(postId);
                return Ok(jobPost);
            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByJobId/{jobId}")]
        public async Task<ActionResult> GetByJobId(string jobId)
        {
            try
            {
                List<JobPost> jobPosts = await jobRepo.GetJobPostsByJobIdAsync(jobId);
                return Ok(jobPosts);
            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(JobPost jobPost)
        {
            try
            {
                await jobRepo.AddJobPostAsync(jobPost);
                return Created($"api/JobPost/{jobPost.PostID}", jobPost);
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{postId}")]
        public async Task<ActionResult> Update(int postId, JobPost jobPost)
        {
            try
            {
                await jobRepo.UpdateJobPostAsync(postId, jobPost);
                return Ok(jobPost);
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult> Delete(int postId)
        {
            try
            {
                await jobRepo.DeleteJobPostAsync(postId);
                return Ok();
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
