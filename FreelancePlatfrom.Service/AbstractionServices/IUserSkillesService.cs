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
        Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds);
        Task AddUserSkillsAsync(string userId, List<int> skillIds);
        Task RemoveUserSkillsAsync(string userId);
        Task<List<int>> GetUserSkillIdsAsync(string userId);
        Task RemoveUserSkillById(string userId,int Skillid);
        Task<List<string>> GetUserSkillNamesAsync(string userId);
        Task<List<Skill>> GetUserSkillsWithNamesAndIdAsync(string userId);


    }
}
