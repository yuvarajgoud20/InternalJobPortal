using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternalJobPortalLibrary;
using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary.Repos;

namespace InternalJobPortalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSkillController : ControllerBase
    {
        readonly IJobSkillRepoAsync jobSkillRepo;

        public JobSkillController(IJobSkillRepoAsync repository)
        {
            jobSkillRepo = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<JobSkill> jobSkills = await jobSkillRepo.GetAllJobSkillsAsync();
            return Ok(jobSkills);
        }

        [HttpGet("{jobID}/{skillID}")]
        public async Task<ActionResult> GetOne(string jobID, string skillID)
        {
            try
            {
                JobSkill jobSkill = await jobSkillRepo.GetJobSkillAsync(jobID, skillID);
                return Ok(jobSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByJobID/{jobID}")]
        public async Task<ActionResult> GetByJobID(string jobID)
        {
            try
            {
                List<JobSkill> jobSkill = await jobSkillRepo.GetJobSkillsByJobIDAsync(jobID);
                return Ok(jobSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BySkillID/{skillID}")]
        public async Task<ActionResult> GetBySkillID(string skillID)
        {
            try
            {
                List<JobSkill> jobSkill = await jobSkillRepo.GetJobSkillsBySkillIDAsync(skillID);
                return Ok(jobSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(JobSkill jobSkill)
        {
            try
            {
                await jobSkillRepo.AddJobSkillAsync(jobSkill);
                return Created($"api/JobSkill/{jobSkill.JobID}/{jobSkill.SkillID}", jobSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{jobID}/{skillID}")]
        public async Task<ActionResult> Update(string jobID, string skillID, JobSkill jobSkill)
        {
            try
            {
                await jobSkillRepo.UpdateJobSkillAsync(jobID, skillID, jobSkill);
                return Ok(jobSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{jobID}/{skillID}")]
        public async Task<ActionResult> Delete(string jobID, string skillID)
        {
            try
            {
                await jobSkillRepo.DeleteJobSkillAsync(jobID, skillID);
                return Ok();
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
