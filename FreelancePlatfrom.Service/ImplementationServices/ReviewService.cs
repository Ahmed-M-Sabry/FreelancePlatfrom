using FreelancePlatfrom.Data.Entities.Rating;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task AddReviewAsync(Review review)
        {
            return _reviewRepository.AddAsync(review);
        }

        public async Task<bool> CheckIfReviewExists(int contractId, string clientId, string freelancerId)
        {
            return await _reviewRepository.CheckIfReviewExists(contractId, clientId, freelancerId);
        }

        public Task DeleteReviewAsync(Review review)
        {
            return _reviewRepository.DeleteAsync(review);
        }

        public async Task<List<Review>> GetAllMyReviewsAsync(string userId)
        {
            return await _reviewRepository.GetAllMyReviewsAsync(userId);
        }

        public async Task<Review> GetByIdAndUserIdAsync(int id, string userId)
        {
            return await _reviewRepository.GetByIdAndUserIdAsync(id, userId);
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<Review> GetReviewWithUsersAsync(int reviewId)
        {
            return await _reviewRepository.GetReviewWithUsersAsync(reviewId);
        }

        public Task UpdateReview(Review review)
        {
            return _reviewRepository.UpdateAsync(review);
        }
    }
}
