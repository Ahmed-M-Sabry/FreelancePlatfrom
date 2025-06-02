using FreelancePlatfrom.Data.Entities.JobPostAndContract;
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
    public class jobPostRepository : GenericRepositoryAsync<JobPost>, IjobPostRepository
    {
        private readonly ApplicationDbContext _context;

        public jobPostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<JobPost> DeleteJobPost(string UserId, int id)
        {
            var oldData = await _context.JobPosts
                            .FirstOrDefaultAsync(j => j.UserId == UserId && j.Id == id && !j.IsDeleted);

            oldData.IsDeleted = true;
            _context.JobPosts.Update(oldData);
            await _context.SaveChangesAsync();
            return oldData;
        }
        public async Task<string> DeleteAsync(JobPost jobPost)
        {
            _context.JobPosts.Remove(jobPost);
            return await _context.SaveChangesAsync() > 0 ? "Job Post Deleted Successfully" : "Failed to Delete Job Post";
        }
        public async Task<List<JobPost>> GetAllJobPostsAsync()
        {
            return await _context.JobPosts.Where(j => !j.IsDeleted)
                //--
                .Include(j=>j.ApplicationUser)
                .Include(j => j.Category)
                .Include(j => j.JobPostSkills)
                    .ThenInclude(js => js.Skill).ToListAsync();
        }

        public async Task<JobPost> GetJobPostByIdAsync(string userId, int id)
        {
            return await _context.JobPosts
                .Include(j => j.Category)
                .Include(j => j.JobPostSkills)
                    .ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(j => j.UserId == userId && j.Id == id && !j.IsDeleted);
        }

        public async Task<List<JobPost>> GetMyJobPostsAsync(string userId)
        {
            return await _context.JobPosts
                .Where(j => j.UserId == userId && !j.IsDeleted)
                .Include(j => j.Category)
                .Include(j => j.JobPostSkills)
                .ThenInclude(js => js.Skill)
                .ToListAsync();
        }
        public async Task<JobPost> GetByIdAsync(int id)
        {
            return await _context.JobPosts
                .Include(j => j.ApplicationUser)
                .Include(j => j.Category)
                .Include(j => j.JobPostSkills)
                    .ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(j => j.Id == id && !j.IsDeleted);
        }

        public async Task<JobPost> GetByIdAndNotDeletedAsync(int id)
        {
            return await _context.JobPosts
                .FirstOrDefaultAsync(j => j.Id == id && !j.IsDeleted);
        }
        public async Task<List<JobPost>> GetFavoriteJobPostsAsync(string freelancerId)
        {
            return await _context.FavoriteJobPost
                .Where(f => f.FreelancerId == freelancerId)
                .Include(f => f.JobPost)
                .Select(f => f.JobPost)
                .ToListAsync();
        }
        public async Task<List<JobPost>> SearchJobPostsAsync(string keyword)
        {
            keyword = keyword?.Trim().ToLower();

            return await _context.JobPosts
                .Where(j => !j.IsDeleted &&
                            (EF.Functions.Like(j.Title.ToLower(), $"%{keyword}%") ||
                             EF.Functions.Like(j.Description.ToLower(), $"%{keyword}%")))
                .ToListAsync();
        }

    }

}
