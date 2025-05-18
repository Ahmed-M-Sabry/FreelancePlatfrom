using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class UserSkillesService : IUserSkillesService
    {
        private readonly ISkillRepository _skillRepository;
        public UserSkillesService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }


        public async Task AddUserSkillsAsync(string userId, List<int> skillIds)
        {
            await _skillRepository.AddUserSkillsAsync(userId, skillIds);
        }

        public async Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds)
        {
            return await _skillRepository.GetValidSkillIdsAsync(skillIds);
        }

        public Task RemoveUserSkillsAsync(string userId)
        {
            return  _skillRepository.RemoveUserSkillsAsync(userId);
        }
        public async Task<List<int>> GetUserSkillIdsAsync(string userId)
        {
            return await _skillRepository.GetUserSkillIdsAsync(userId);
        }

        public Task RemoveUserSkillById(string userId, int Skillid)
        {
            return _skillRepository.RemoveUserSkillById(userId, Skillid);
        }
        public async Task<List<string>> GetUserSkillNamesAsync(string userId)
        {
            return await _skillRepository.GetUserSkillNamesAsync(userId);
        }

        public async Task<List<Skill>> GetUserSkillsWithNamesAndIdAsync(string userId)
        {
            return await _skillRepository.GetUserSkillsWithNamesAndIdAsync(userId);
        }
    }
}
