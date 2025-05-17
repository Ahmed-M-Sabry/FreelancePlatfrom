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
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds)
        {
            return await _skillRepository.GetValidSkillIdsAsync(skillIds);
        }
    }
}
