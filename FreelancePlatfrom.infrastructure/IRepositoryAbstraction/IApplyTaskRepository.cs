using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IApplyTaskRepository : IGenericRepositoryAsync<ApplyTask>
    {     
        Task<ApplyTask> GetByJobPostIdAndFreelancerIdAsync(int jobPostId, string userId);
        Task<ApplyTask> GetApplyTask(string userId, int id);
        Task<List<ApplyTask>> GetAllApplyTask(string userId);
        //Task DeleteApplyTask(ApplyTask applyTask);
        Task<ApplyTask> GetApplyTaskById(string userId, int id);
        Task<ApplyTask> AcceptApplyTask(string userId, int id);
        Task<ApplyTask> RejectApplyTask(string userId, int id);
        Task<List<ApplyTask>> GetAcceptedApplyTaskForFreelancer(string userId);
        Task<List<ApplyTask>> GetRejectedApplyTaskForFreelancer(string userId);
        Task<List<ApplyTask>> GetPendingApplyTaskForFreelancer(string userId);
    }

}
