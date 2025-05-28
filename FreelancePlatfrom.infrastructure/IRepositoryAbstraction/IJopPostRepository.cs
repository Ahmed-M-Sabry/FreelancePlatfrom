using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IjobPostRepository : IGenericRepositoryAsync<JobPost>
    {
        Task<List<JobPost>> GetMyJobPostsAsync(string UserId);

        Task<JobPost> GetJobPostByIdAsync(string UserId,int id);

        Task<JobPost> DeleteJobPost(string UserId, int id);

        Task<List<JobPost>> GetAllJobPostsAsync();

        Task<JobPost> GetByIdAsync(int id);

        Task<JobPost> GetByIdAndNotDeletedAsync(int id);
        Task<string> DeleteAsync(JobPost jobPost);

        Task<List<JobPost>> GetFavoriteJobPostsAsync(string freelancerId);

    }
}
