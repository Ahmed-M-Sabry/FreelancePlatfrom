using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IJobPostFavouriteRepository : IGenericRepositoryAsync<FavJobPost>
    {
        Task<FavJobPost> IsJobPostFavorited(string freelancerId, int jobPostId);
        //   Task<List<string>> GetAllJobPostsFavoritedByClient(string clientId);
        // Task<List<string>> GetAllClientsWhoFavoritedJobPost(string jobPostId);

        Task<List<FavJobPost>> GetFavoriteJobPostsAsync(string freelancerId);

    }
}
