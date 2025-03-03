using InternalJobPortalLibrary;
using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternalJobPortalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSkillController : ControllerBase
    {
        readonly IEmployeeSkillRepoAsync empSkillRepo;
        public EmployeeSkillController(IEmployeeSkillRepoAsync repository)
        {
            empSkillRepo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<EmployeeSkill> employeeSkills = await empSkillRepo.GetAllEmployeeSkillsAsync();
            return Ok(employeeSkills);
        }
        [HttpGet("{empID}/{skillID}")]
        public async Task<ActionResult> GetOne(string empID, string skillID)
        {
            try
            {

                EmployeeSkill empSkill = await empSkillRepo.GetEmployeeSkillAsync(empID, skillID);
                return Ok(empSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByEmployeeID/{empID}")]
        public async Task<ActionResult> GetByEmployeeID(string empID)
        {
            try
            {
                List<EmployeeSkill> employeeSkills = await empSkillRepo.GetEmployeeSkillsByEmployeeIDAsync(empID);
                return Ok(employeeSkills);
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
                List<EmployeeSkill> employeeSkills = await empSkillRepo.GetEmployeeSkillsBySkillIDAsync(skillID);
                return Ok(employeeSkills);
            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(EmployeeSkill empSkill)
        {
            try
            {
                await empSkillRepo.AddEmployeeSkillAsync(empSkill);
                return Created($"api/EmployeeSkill/{empSkill.EmployeeID}/{empSkill.SkillID}", empSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{empID}/{skillID}")]
        public async Task<ActionResult> Update(string empID, string skillID, EmployeeSkill empSkill)
        {
            try
            {
                await empSkillRepo.UpdateEmployeeSkillAsync(empID, skillID, empSkill);
                return Ok(empSkill);
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{empID}/{skillID}")]
        public async Task<ActionResult> Delete(string empID, string skillID)
        {
            try
            {
                await empSkillRepo.DeleteEmployeeSkillAsync(empID, skillID);
                return Ok();
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
