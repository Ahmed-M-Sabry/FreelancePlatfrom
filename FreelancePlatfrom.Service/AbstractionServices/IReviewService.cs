using FreelancePlatfrom.Data.Entities.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IReviewService
    {
        Task AddReviewAsync(Review review);
        Task<bool> CheckIfReviewExists(int contractId, string clientId, string freelancerId);
        Task UpdateReview(Review review);
        Task<Review> GetByIdAsync(int id);
        Task DeleteReviewAsync(Review review);
        Task<Review> GetByIdAndUserIdAsync(int id, string userId);
        Task<Review> GetReviewWithUsersAsync(int reviewId);
        Task<List<Review>> GetAllMyReviewsAsync(string userId);

        Task<double> GetFreelancerRateById(string freelancerId);


    }
}
