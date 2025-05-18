
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelancePlatform.Infrastructure.RepositoryImplemention
{
    public class JobPostSkillServicesRepository : GenericRepositoryAsync<JobPostSkill>, IJobPostSkillServicesRepository
    {
        private readonly ApplicationDbContext _context;

        public JobPostSkillServicesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<int>> GetSkillsByJobPostIdAsync(int jobPostId)
        {
            return await _context.JobPostSkill
                .Where(jps => jps.JobPostId == jobPostId)
                .Select(jps => jps.SkillId)
                .ToListAsync();
        }

        public async Task RemoveSkillsByJobPostIdAsync(int jobPostId)
        {
            var skills = await _context.JobPostSkill
                .Where(jps => jps.JobPostId == jobPostId)
                .ToListAsync();
            _context.JobPostSkill.RemoveRange(skills);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<JobPostSkill> jobPostSkills)
        {
            await _context.JobPostSkill.AddRangeAsync(jobPostSkills);
            await _context.SaveChangesAsync();
        }
    }
}