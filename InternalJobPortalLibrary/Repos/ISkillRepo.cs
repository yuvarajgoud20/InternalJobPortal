using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalJobPortalLibrary.Models;

namespace InternalJobPortalLibrary.Repos
{
    public interface ISkillRepo
    {
        Task<List<Skill>> GetAllSkills();
        Task<Skill> GetSkill(string sid);
        Task InsertSkill(Skill skill);
        Task UpdateSkill(string sid, Skill skill);
        Task DeleteSkill(string sid);
    }
}
