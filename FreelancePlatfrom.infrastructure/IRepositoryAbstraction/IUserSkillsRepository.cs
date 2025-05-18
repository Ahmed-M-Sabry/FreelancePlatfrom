using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IUserSkillsRepository : IGenericRepositoryAsync<UserSkill>
    {
        Task RemoveUserSkillsAsync(string userId);
        Task AddUserSkillsAsync(string userId, List<int> skillIds);
        Task<List<int>> GetUserSkillIdsAsync(string userId);
        Task RemoveUserSkillById(string userId, int Skillid);
        Task<List<string>> GetUserSkillNamesAsync(string userId);
        Task<List<Skill>> GetUserSkillsWithNamesAndIdAsync(string userId);



    }
}
