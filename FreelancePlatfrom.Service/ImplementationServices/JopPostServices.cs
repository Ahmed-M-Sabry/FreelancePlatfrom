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
    public class jobPostServices : IjobPostServices
    {
        private readonly IjobPostRepository _jobPostRepository;
        public jobPostServices(IjobPostRepository jobPostRepository)
        {
            _jobPostRepository = jobPostRepository;
        }

        public Task AddJobPost(JobPost jobPost)
        {
            return _jobPostRepository.AddAsync(jobPost);
        }

        public async Task<JobPost> DeleteJobPost(string UserId, int id)
        {
            return await _jobPostRepository.DeleteJobPost(UserId, id);
        }

        public async Task<List<JobPost>> GetAllJobPostsAsync()
        {
           return await _jobPostRepository.GetAllJobPostsAsync();
        }

        public async Task<JobPost> GetByIdAsync(int id)
        {
            return await _jobPostRepository.GetByIdAsync(id);
        }

        public async Task<JobPost> GetJobPostByIdAsync(string UserId, int id)
        {
            return await _jobPostRepository.GetJobPostByIdAsync(UserId, id);
        }

        public async Task<List<JobPost>> GetMyJobPostsAsync(string UserId)
        {
            return await _jobPostRepository.GetMyJobPostsAsync(UserId);
        }

        public Task UpdateJobPostAsync(JobPost jobPost)
        {
            return _jobPostRepository.UpdateAsync(jobPost);
        }
    }
}
