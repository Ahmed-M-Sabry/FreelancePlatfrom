using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelancePlatform.Service.AbstractionServices
{
    public interface IJobPostSkillServices
    {
        Task<List<int>> GetSkillsByJobPostIdAsync(int jobPostId);
        Task RemoveSkillsByJobPostIdAsync(int jobPostId);
        Task AddRangeAsync(IEnumerable<JobPostSkill> jobPostSkills);
    }
}