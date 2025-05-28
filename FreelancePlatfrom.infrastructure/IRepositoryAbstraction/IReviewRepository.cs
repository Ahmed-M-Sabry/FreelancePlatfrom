using FreelancePlatfrom.Data.Entities.Rating;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IReviewRepository : IGenericRepositoryAsync<Review>
    {
        Task<bool> CheckIfReviewExists(int contractId, string clientId , string freelancerId);
        Task<Review> GetByIdAndUserIdAsync(int id , string userId);
        Task<Review> GetReviewWithUsersAsync(int reviewId);
        Task<List<Review>> GetAllMyReviewsAsync(string userId);

        // Additional methods can be added here as needed
        Task<double> GetFreelancerRateById(string freelancerId);

    }
}
