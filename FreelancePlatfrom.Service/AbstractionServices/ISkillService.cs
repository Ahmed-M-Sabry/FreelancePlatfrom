using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface ISkillService
    {
        Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds);
        Task<Skill> GetByIdAsync(int id);
        Task<Skill> GetByNameAsync(string Skillname);
        Task CreateNewSkill(Skill newSkill);
        Task UpdatedCategory(Skill skill);
        Task<Skill> DeleteSkill(int id);
        Task<Skill> RestoreSkill(int id);
        Task<List<Skill>> GetAllSKillForUserAsync();
        Task<List<Skill>> GetAllSKillForAdminAsync();

    }
}
