using FreelancePlatfrom.Data.Entities.RegisterNeeded;
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
        private readonly IUserSkillsRepository _skillRepository;
        public UserSkillesService(IUserSkillsRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }


        public async Task AddUserSkillsAsync(string userId, List<int> skillIds)
        {
            await _skillRepository.AddUserSkillsAsync(userId, skillIds);
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

        public Task AddUserSkillsAsync(List<UserSkill> newUserSkills)
        {
            return _skillRepository.AddRangeAsync(newUserSkills);
        }
        public async Task RemoveUserSkillsAsync(string userId, List<int> skillIds)
        {
            foreach (var skillId in skillIds)
            {
                await _skillRepository.RemoveUserSkillById(userId, skillId);
            }
        }

    }
}
