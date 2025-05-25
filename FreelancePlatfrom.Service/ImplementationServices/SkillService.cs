using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.infrastructure.RepositoryImplemention;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepostitory _skillRepostitory;
        public SkillService(ISkillRepostitory skillRepostitory)
        {
            _skillRepostitory = skillRepostitory;
        }

        public Task CreateNewSkill(Skill newSkill)
        {
            return _skillRepostitory.AddAsync(newSkill);
        }

        public async Task<Skill> DeleteSkill(int id)
        {
            return await _skillRepostitory.DeleteSkill(id);
        }

        public async Task<List<Skill>> GetAllSKillForAdminAsync()
        {
            return await _skillRepostitory.GetAllSKillForAdminAsync();
        }

        public async Task<List<Skill>> GetAllSKillForUserAsync()
        {
            return await _skillRepostitory.GetAllSKillForUserAsync();
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _skillRepostitory.GetByIdAsync(id);
        }

        public async Task<Skill> GetByNameAsync(string Skillname)
        {
            return await _skillRepostitory.GetByNameAsync(Skillname);
        }

        public async Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds)
        {
            return await _skillRepostitory.GetValidSkillIdsAsync(skillIds);
        }

        public async Task<Skill> RestoreSkill(int id)
        {
            return await _skillRepostitory.RestoreSkill(id);
        }

        public Task UpdatedCategory(Skill skill)
        {
            return _skillRepostitory.UpdateAsync(skill);
        }
    }
}
