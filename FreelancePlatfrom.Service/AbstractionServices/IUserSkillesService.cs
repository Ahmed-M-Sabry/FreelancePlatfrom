using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IUserSkillesService
    {
        Task AddUserSkillsAsync(string userId, List<int> skillIds);
        Task RemoveUserSkillsAsync(string userId);
        Task<List<int>> GetUserSkillIdsAsync(string userId);
        Task RemoveUserSkillById(string userId,int Skillid);
        Task<List<string>> GetUserSkillNamesAsync(string userId);
        Task<List<Skill>> GetUserSkillsWithNamesAndIdAsync(string userId);
        Task AddUserSkillsAsync(List<UserSkill> newUserSkills);
        Task RemoveUserSkillsAsync(string userId, List<int> skillIds);

    }
}
