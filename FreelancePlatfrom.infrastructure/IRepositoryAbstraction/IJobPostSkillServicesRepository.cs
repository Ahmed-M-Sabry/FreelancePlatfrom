using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IJobPostSkillServicesRepository : IGenericRepositoryAsync<JobPostSkill>
    {
        Task<List<int>> GetSkillsByJobPostIdAsync(int jobPostId);
        Task RemoveSkillsByJobPostIdAsync(int jobPostId);
        Task AddRangeAsync(IEnumerable<JobPostSkill> jobPostSkills);
    }
    
}
