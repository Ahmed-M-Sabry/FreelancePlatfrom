using FreelancePlatfrom.Data.Entities.FavoritesTables;
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
    public class FavoriteJobPostService : IFavoriteJobPostService
    {
        private readonly IJobPostFavouriteRepository _jobPostFavouriteRepository;
        public FavoriteJobPostService(IJobPostFavouriteRepository jobPostFavouriteRepository)
        {
            _jobPostFavouriteRepository = jobPostFavouriteRepository;
        }

        public Task AddJobPostFavourite(FavJobPost favJobPost)
        {
            return _jobPostFavouriteRepository.AddAsync(favJobPost);
        }

        public async Task<List<FavJobPost>> GetFavoriteJobPostsAsync(string freelancerId)
        {
            return await _jobPostFavouriteRepository.GetFavoriteJobPostsAsync(freelancerId);
        }

        public async Task<FavJobPost> IsJobPostFavorited(string freelancerId, int jobPostId)
        {
            return await _jobPostFavouriteRepository.IsJobPostFavorited(freelancerId, jobPostId);
        }

        public Task RemoveJobPostFavourite(FavJobPost favJobPost)
        {
            return _jobPostFavouriteRepository.DeleteAsync(favJobPost);
        }
    }
}
