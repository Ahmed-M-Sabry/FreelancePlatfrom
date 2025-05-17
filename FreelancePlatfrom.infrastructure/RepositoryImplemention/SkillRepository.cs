using FreelancePlatfrom.Data.Entities.RegisterNeeded;
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
    public class SkillRepository :GenericRepositoryAsync<UserSkill> ,ISkillRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillRepository(ApplicationDbContext context) : base(context)
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
        public async Task RemoveUserSkillsAsync(string userId)
        {
            var userSkills = await _context.UserSkills
                .Where(us => us.UserId == userId)
                .ToListAsync();

            if (userSkills.Any())
            {
                _context.UserSkills.RemoveRange(userSkills);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddUserSkillsAsync(string userId, List<int> skillIds)
        {
            if (skillIds == null || !skillIds.Any())
                return;

            var userSkills = skillIds.Select(skillId => new UserSkill
            {
                UserId = userId,
                SkillId = skillId
            }).ToList();

            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();
        }
        public async Task<List<int>> GetUserSkillIdsAsync(string userId)
        {
            return await _context.UserSkills
                .Where(us => us.UserId == userId)
                .Select(us => us.SkillId)
                .ToListAsync();
        }

        public async Task RemoveUserSkillById(string userId, int skillId)
        {
            var skill = await _context.UserSkills
                .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId);

            if (skill != null)
            {
                _context.UserSkills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetUserSkillNamesAsync(string userId)
        {
            return await _context.UserSkills
                .Where(us => us.UserId == userId)
                .Include(us => us.Skill) 
                .Select(us => us.Skill.Name)
                .ToListAsync();
        }

        public async Task<List<Skill>> GetUserSkillsWithNamesAndIdAsync(string userId)
        {
            return await _context.UserSkills
                .Where(us => us.UserId == userId)
                .Select(us => new Skill
                {
                    Id = us.Skill.Id,
                    Name = us.Skill.Name
                })
                .ToListAsync();
        }

    }
}
