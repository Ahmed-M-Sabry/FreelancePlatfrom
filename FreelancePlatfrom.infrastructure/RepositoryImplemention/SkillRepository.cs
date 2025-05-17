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
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillRepository(ApplicationDbContext context)
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
