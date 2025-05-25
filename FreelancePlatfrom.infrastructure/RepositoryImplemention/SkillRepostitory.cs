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

        public async Task<Skill> DeleteSkill(int id)
        {
            var Data = await _context.Skills.FirstOrDefaultAsync(i=>i.Id == id && !i.IsDeleted);
            Data.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Data;
        }

        public async Task<List<Skill>> GetAllSKillForAdminAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<List<Skill>> GetAllSKillForUserAsync()
        {
            return await _context.Skills.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<Skill> GetByNameAsync(string Skillname)
        {
            return await _context.Skills
                .FirstOrDefaultAsync(s => s.Name.ToLower() == Skillname.ToLower());
        }

        public async Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds)
        {
            return await _context.Skills
                .Where(s => skillIds.Contains(s.Id))
                .Select(s => s.Id)
                .ToListAsync();
        }

        public async Task<Skill> RestoreSkill(int id)
        {
            var Data = await _context.Skills.FirstOrDefaultAsync(i => i.Id == id);
            Data.IsDeleted = false;
            await _context.SaveChangesAsync();
            return Data;
        }
    }
}
