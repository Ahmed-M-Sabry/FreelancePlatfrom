using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IApplyTaskService
    {
        Task<string> AddAsync(ApplyTask applyTask);
        Task<string> UpdateAsync(ApplyTask applyTask);
        Task<ApplyTask> GetByJobPostIdAndFreelancerIdAsync(int jobPostId, string userId);
        Task<ApplyTask> GetApplyTask(string userId, int id);
        Task<string> DeleteApplyTask(ApplyTask applyTask);
        Task<List<ApplyTask>> GetAllApplyTask(string userId);
        Task<ApplyTask> GetApplyTaskById(string userId, int id);
        Task<ApplyTask> RejectApplyTask(string userId, int id);
        Task<ApplyTask> AcceptApplyTask(string userId, int id);
        Task<bool> isApplyTaskAccepted(string userId, int id);
        Task<bool> isApplyTaskRejected(string userId, int id);
        Task<List<ApplyTask>> GetAcceptedApplyTaskForFreelancer(string userId);
        Task<List<ApplyTask>> GetRejectedApplyTaskForFreelancer(string userId);
        Task<List<ApplyTask>> GetPendingApplyTaskForFreelancer(string userId);


    }
}
