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
        public async Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds)
        {
            return await _skillRepostitory.GetValidSkillIdsAsync(skillIds);
        }

    }
}
