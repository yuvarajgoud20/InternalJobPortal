using InternalJobPortalLibrary;
using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternalJobPortalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        readonly IApplyJobRepo applyJobRepo;
        public ApplicationController(IApplyJobRepo repo)
        {
            applyJobRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<ApplyJob> applications = await applyJobRepo.GetAllApplyJobs();
            return Ok(applications);
        }
        [HttpGet("{postID}/{employeeID}")]
        public async Task<ActionResult> GetOne(int postID,string employeeID)
        {
            try
            {
                ApplyJob applyJob = await applyJobRepo.GetApplyJob(postID,employeeID);
                return Ok(applyJob);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(ApplyJob applyJob)
        {
            try
            {
                await applyJobRepo.InsertApplyJob(applyJob);
                return Created($"api/Application",applyJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{postID}/{employeeID}")]
        public async Task<ActionResult> Update(int postID, string employeeID ,ApplyJob applyJob)
        {
            try
            {
                await applyJobRepo.UpdateApplyJob(postID, employeeID , applyJob);
                return Ok(applyJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{postID}/{employeeID}")]
        public async Task<ActionResult> Delete(int postID, string employeeID)
        {
            try
            {
                await applyJobRepo.DeleteApplyJob(postID,employeeID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByPostId/{postID}")]
        public async Task<ActionResult> ByPostId(int postID)
        {
            try
            {
                List<ApplyJob> applyJobs = await applyJobRepo.GetApplyJobsByPostID(postID);
                return Ok(applyJobs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
