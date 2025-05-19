using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IjobPostServices
    {
        Task AddJobPost(JobPost jobPost);
        Task<JobPost> GetByIdAsync(int id);
        Task UpdateJobPostAsync(JobPost jobPost);
        Task<List<JobPost>> GetAllJobPostsAsync();
        Task<List<JobPost>> GetMyJobPostsAsync(string UserId);
        Task<JobPost> GetJobPostByIdAsync(string UserId, int id);

        Task<JobPost> DeleteJobPost(string UserId, int id);

        Task<JobPost> GetByIdAndNotDeletedAsync(int id);
        Task<string> DeleteAsync(JobPost jobPost);

    }
}
