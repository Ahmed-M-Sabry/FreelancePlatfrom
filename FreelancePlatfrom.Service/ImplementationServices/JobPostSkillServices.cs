
using FreelancePlatform.Service.AbstractionServices;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelancePlatform.Infrastructure.Services
{
    public class JobPostSkillServices : IJobPostSkillServices
    {
        private readonly IJobPostSkillServicesRepository _jobPostSkillServicesRepository;

        public JobPostSkillServices(IJobPostSkillServicesRepository jobPostSkillServicesRepository)
        {
            _jobPostSkillServicesRepository = jobPostSkillServicesRepository;
        }

        public async Task<List<int>> GetSkillsByJobPostIdAsync(int jobPostId)
        {
            return await _jobPostSkillServicesRepository.GetSkillsByJobPostIdAsync(jobPostId);
        }

        public async Task RemoveSkillsByJobPostIdAsync(int jobPostId)
        {
            await _jobPostSkillServicesRepository.RemoveSkillsByJobPostIdAsync(jobPostId);
        }

        public async Task AddRangeAsync(IEnumerable<JobPostSkill> jobPostSkills)
        {
            await _jobPostSkillServicesRepository.AddRangeAsync(jobPostSkills);
        }
    }
}