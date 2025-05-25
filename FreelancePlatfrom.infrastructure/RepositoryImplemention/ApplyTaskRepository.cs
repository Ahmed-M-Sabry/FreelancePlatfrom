using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.RepositoryImplemention
{
    public class ApplyTaskRepository : GenericRepositoryAsync<ApplyTask> , IApplyTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplyTaskRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ApplyTask>> GetAllApplyTask(string userId)
        {
            return await _context.ApplyTasks
                .Where(a => a.FreelancerId == userId || a.ClientId == userId && !a.IsDeleted)
                .Include(a => a.JobPost)
                .Include(a=>a.Freelancer)
                .ToListAsync();
        }

        //public Task DeleteApplyTask(ApplyTask applyTask)
        //{
        //    applyTask.IsDeleted = true;
        //    _context.Update(applyTask);
        //    return _context.SaveChangesAsync();
        //}

        public async Task<ApplyTask> GetApplyTask(string userId, int id)
        {
            return await _context.ApplyTasks
                 .FirstOrDefaultAsync(a => a.FreelancerId == userId || a.ClientId == userId && a.Id == id && !a.IsDeleted);
        }

        public async Task<ApplyTask> GetApplyTaskById(string userId, int id)
        {
            return await _context.ApplyTasks
                .Include(a => a.JobPost)
                .Include(a => a.Freelancer)
                .FirstOrDefaultAsync(a=>a.FreelancerId == userId  || a.ClientId == userId && a.Id == id && !a.IsDeleted);
        }

        public async Task<ApplyTask> GetByJobPostIdAndFreelancerIdAsync(int jobPostId, string userId)
        {
            return await _context.ApplyTasks
                .FirstOrDefaultAsync(a => a.JobPostId == jobPostId && a.FreelancerId == userId && !a.IsDeleted);
        }


        public async Task<ApplyTask> RejectApplyTask(string userId, int id)
        {
            var oldData =  await _context.ApplyTasks
                                .FirstOrDefaultAsync(a => a.ClientId == userId && a.Id == id && !a.IsDeleted);
            oldData.Status = ApplyTaskStatus.Rejected;
            await _context.SaveChangesAsync();
            return oldData;
        }

        public async Task<ApplyTask> AcceptApplyTask(string userId, int id)
        {
            var oldData = await _context.ApplyTasks
                                .FirstOrDefaultAsync(a => a.ClientId == userId && a.Id == id && !a.IsDeleted);
            oldData.Status = ApplyTaskStatus.Accepted;
            await _context.SaveChangesAsync();
            return oldData;

        }

        public async Task<List<ApplyTask>> GetAcceptedApplyTaskForFreelancer(string userId)
        {
            return await _context.ApplyTasks
                .Where(a => a.FreelancerId == userId && a.Status == ApplyTaskStatus.Accepted && !a.IsDeleted)
                .Include(a => a.JobPost)
                .Include(a => a.Client)
                .ToListAsync();
        }

        public async Task<List<ApplyTask>> GetRejectedApplyTaskForFreelancer(string userId)
        {
            return await _context.ApplyTasks
                .Where(a => a.FreelancerId == userId && a.Status == ApplyTaskStatus.Rejected && !a.IsDeleted)
                .Include(a => a.JobPost)
                .Include(a => a.Client)
                .ToListAsync();
        }

        public async Task<List<ApplyTask>> GetPendingApplyTaskForFreelancer(string userId)
        {
            return await _context.ApplyTasks
                .Where(a => a.FreelancerId == userId && a.Status == ApplyTaskStatus.Pending && !a.IsDeleted)
                .Include(a => a.JobPost)
                .Include(a => a.Client)
                .ToListAsync();
        }

        public async Task<ApplyTask> GetApplyTaskBetweenClientAndFreelancer(string Clinetid, string FreelacnerId, int id)
        {
            return await _context.ApplyTasks
                .FirstOrDefaultAsync(i=>i.FreelancerId == FreelacnerId  && i.Id == id && i.ClientId == Clinetid);
        }
    }
}
