using InternalJobPortalLibrary.Models;
using InternalJobPortalLibrary.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternalJobPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        readonly ISkillRepo skillRepo;
        public SkillController(ISkillRepo repository)
        {
            skillRepo = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<Skill> skills = await skillRepo.GetAllSkills();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("{sid}")]
        public async Task<ActionResult> Getone(string sid)
        {
            try
            {
                Skill skill = await skillRepo.GetSkill(sid);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Skill skill)
        {
            try
            {
                await skillRepo.InsertSkill(skill);
                return Created($"api/Skill/{skill.SkillID}", skill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{sid}")]
        public async Task<ActionResult> Update(string sid, Skill skill)
        {
            try
            {
                await skillRepo.UpdateSkill(sid, skill);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{sid}")]
        public async Task<ActionResult> Delete(string sid)
        {
            try
            {
                await skillRepo.DeleteSkill(sid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
