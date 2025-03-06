using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternalJobPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class JobController : ControllerBase
    {
        readonly IJobRepoAsync jobRepo;     //Declares a readonly field of type
        public JobController(IJobRepoAsync repository) //This is the constructor for the JobController class. It takes an instance of IJobRepoAsync as a parameter.
        {
            jobRepo = repository;          //Assigns the passed repository instance to the jobRepo field.
        }
        [HttpGet]                          //This attribute specifies that the GetAll method will be called when an HTTP GET request is made to the specified route.
        public async Task<ActionResult> GetAll()         //This method returns all the jobs in the database.
        {
            try
            {
                List<Job> jobs = await jobRepo.GetAllJobs(); //Calls the GetAllJobsAsync method of the jobRepo field to get all the jobs.
                return Ok(jobs);                             //Returns the list of jobs as an HTTP 200 response.
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("{jid}")] //This attribute specifies that the Getone method will be called when an HTTP GET request is made to the specified route.
        public async Task<ActionResult> Getone(string jid) //This method returns a specific job based on the job ID.
        {
            try
            {
                Job job = await jobRepo.GetJob(jid); //Calls the GetJobAsync method of the jobRepo field to get the job with the specified job ID.
                return Ok(job);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Job job)
        {
            try
            {
                await jobRepo.InsertJob(job);
                return Created($"api/Job/{job.JobID}", job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
        [HttpPut("{jid}")]
        public async Task<ActionResult> Update(string jid, Job job)
        {
            try
            {
                await jobRepo.UpdateJob(jid, job);
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{jid}")]
        public async Task<ActionResult> Delete(string jid)
        {
            try
            {
                await jobRepo.DeleteJob(jid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
