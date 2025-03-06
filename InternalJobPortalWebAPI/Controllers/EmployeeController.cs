using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternalJobPortalLibrary.Repos;
using InternalJobPortalLibrary;
using Microsoft.AspNetCore.Authorization;

namespace InternalJobPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        readonly IEmployeeRepoAsync empRepo;
        public EmployeeController(IEmployeeRepoAsync repository)
        {
            empRepo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Employee> employees = await empRepo.GetAllEmployeesAsync();
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            try
            {
                Employee employee = await empRepo.GetEmployeeAsync(id);
                return Ok(employee);

            }
            catch (InternalJobPortalException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Employee employee)
        {
            try
            {
                await empRepo.InsertEmployeeAsync(employee);
                return Created($"api/Employee/{employee.EmployeeID}", employee);
            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Employee employee)
        {
            try
            {
                await empRepo.UpdateEmployeeAsync(id, employee);
                return Ok(employee);

            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await empRepo.DeleteEmployeeAsync(id);
                return Ok();


            }
            catch (InternalJobPortalException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Employee/ByJobId/{JobID}")]

        public async Task<ActionResult> GetByJobId(string JobID)
        {
            try
             {
                List<Employee> jobPosts = await empRepo.GetEmployeeByJobIDAsync(JobID);
                return Ok(jobPosts);
            }

            catch (InternalJobPortalException ex)
            {

                return NotFound(ex.Message);
            }

        }

    }
}
