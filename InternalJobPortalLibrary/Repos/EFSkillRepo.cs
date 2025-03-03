using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace InternalJobPortalLibrary.Repos
{
    public class EFSkillRepo : ISkillRepo
    {
        InternalJobPortalDBContext ctx = new InternalJobPortalDBContext();
        public async Task DeleteSkill(string sid)
        {
            try
            {
                Skill sdel = await GetSkill(sid);
                ctx.Skills.Remove(sdel);
                ctx.SaveChanges();

            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in deleting skill");
            }
        }
        public async Task<List<Skill>> GetAllSkills()
        {
            try
            {
                List<Skill> skills = await ctx.Skills.ToListAsync();
                return skills;
            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in getting all skills");
            }
        }

        public async Task<Skill> GetSkill(string sid)
        {

            try
            {
                Skill skill = await (from c in ctx.Skills where c.SkillID == sid select c).FirstAsync();
                return skill;
            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in getting skill");

            }
        }

        public async Task InsertSkill(Skill skill)
        {
            try
            {
                await ctx.Skills.AddAsync(skill);
                ctx.SaveChanges();
            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in inserting skill");
            }
        }

        public async Task UpdateSkill(string sid, Skill skill)
        {
            try
            {
                Skill sedit = await GetSkill(sid);
                sedit.SkillName = skill.SkillName;
                sedit.SkillLevel = skill.SkillLevel;
                ctx.SaveChanges();
            }
            catch (InternalJobPortalException e)
            {
                throw new InternalJobPortalException("Error in updating skill");
            }
        }
    }
}
