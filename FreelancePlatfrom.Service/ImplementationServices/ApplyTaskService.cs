using FreelancePlatfrom.Data.Entities.JobPostAndContract;
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
    }
}
