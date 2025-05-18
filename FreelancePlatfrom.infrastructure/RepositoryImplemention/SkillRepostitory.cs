using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.RepositoryImplemention
{
    public class SkillRepostitory : GenericRepositoryAsync<Skill> , ISkillRepostitory
    {
        private readonly ApplicationDbContext _context;
        public SkillRepostitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds)
        {
            return await _context.Skills
                .Where(s => skillIds.Contains(s.Id))
                .Select(s => s.Id)
                .ToListAsync();
        }
    }
}
