using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class ApplyTaskService : IApplyTaskService
    {
        private readonly IApplyTaskRepository _applyTaskRepository;
        public ApplyTaskService(IApplyTaskRepository applyTaskRepository)
        {
            _applyTaskRepository = applyTaskRepository;
        }

        public async Task<ApplyTask> AcceptApplyTask(string userId, int id)
        {
            return await _applyTaskRepository.AcceptApplyTask(userId, id);
            
        }
        public async Task<ApplyTask> RejectApplyTask(string userId, int id)
        {
            return await _applyTaskRepository.RejectApplyTask(userId, id);
        }
        public async Task<string> AddAsync(ApplyTask applyTask)
        {
            await _applyTaskRepository.AddAsync(applyTask);
            return  "Apply Task Created Successfully";
        }

        public async Task<string> DeleteApplyTask(ApplyTask applyTask)
        {
           await _applyTaskRepository.DeleteAsync(applyTask);

           return "Apply Task Deleted Successfully";
        }

        public async Task<List<ApplyTask>> GetAllApplyTask(string userId)
        {
            return await _applyTaskRepository.GetAllApplyTask(userId);
        }

        public async Task<ApplyTask> GetApplyTask(string userId, int id)
        {
            return await _applyTaskRepository.GetApplyTask(userId, id);
        }

        public async Task<ApplyTask> GetApplyTaskById(string userId, int id)
        {
            return await _applyTaskRepository.GetApplyTaskById(userId, id);
        }

        public async Task<ApplyTask> GetByJobPostIdAndFreelancerIdAsync(int jobPostId, string userId)
        {
            return await _applyTaskRepository.GetByJobPostIdAndFreelancerIdAsync(jobPostId, userId);
        }


        public async Task<string> UpdateAsync(ApplyTask applyTask)
        {
            await _applyTaskRepository.UpdateAsync(applyTask);
            return "Apply Task Updated Successfully";
        }

        public async Task<bool> isApplyTaskAccepted(string userId, int id)
        {
            var applyTask = await _applyTaskRepository.GetApplyTask(userId, id);
            if (applyTask == null)
                return false;
            if(applyTask.Status == ApplyTaskStatus.Accepted)
                return true;
            return false;
        }
        public async Task<bool> isApplyTaskRejected(string userId, int id)
        {
            var applyTask = await _applyTaskRepository.GetApplyTask(userId, id);
            if (applyTask == null)
                return false;
            if (applyTask.Status == ApplyTaskStatus.Rejected)
                return true;
            return false;
        }

        public async Task<List<ApplyTask>> GetAcceptedApplyTaskForFreelancer(string userId)
        {
            return await _applyTaskRepository.GetAcceptedApplyTaskForFreelancer(userId);
        }

        public async Task<List<ApplyTask>> GetRejectedApplyTaskForFreelancer(string userId)
        {
            return await _applyTaskRepository.GetRejectedApplyTaskForFreelancer(userId);
        }
        public async Task<List<ApplyTask>> GetPendingApplyTaskForFreelancer(string userId)
        {
            return await _applyTaskRepository.GetPendingApplyTaskForFreelancer(userId);
        }
    }
}
