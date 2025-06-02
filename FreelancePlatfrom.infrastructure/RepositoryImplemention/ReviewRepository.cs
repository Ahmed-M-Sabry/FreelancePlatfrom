using FreelancePlatfrom.Data.Entities.Rating;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.RepositoryImplemention
{
    public class ReviewRepository : GenericRepositoryAsync<Review> , IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<bool> CheckIfReviewExists(int contractId, string clientId, string freelancerId)
        {
            return await _context.Reviews
                    .AnyAsync(i => i.ContractId == contractId && i.ClientId == clientId && i.FreelancerId == freelancerId);
        }

        public async Task<Review> GetByIdAndUserIdAsync(int id, string userId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id && i.ClientId == userId);
        }

        public async Task<Review> GetReviewWithUsersAsync(int reviewId)
        {
            return await _context.Reviews
                .Include(r => r.Client)
                .Include(r => r.Freelancer)
                .FirstOrDefaultAsync(r => r.Id == reviewId);
        }
        public async Task<List<Review>> GetAllMyReviewsAsync(string userId)
        {
            return await _context.Reviews
                .Include(r => r.Client)
                .Include(r => r.Freelancer)
                .Where(r => r.ClientId == userId || r.FreelancerId == userId)
                .ToListAsync();
        }

        //public async Task<double> GetFreelancerRateById(string freelancerId)
        //{
        //    return _context.Reviews
        //        .Where(r => r.FreelancerId == freelancerId)
        //        .AverageAsync(r => r.Rate).Result;
        //}
        public async Task<double> GetFreelancerRateById(string freelancerId)
        {
            var reviews = await _dbContext.Reviews
                .Where(r => r.FreelancerId == freelancerId)
                .ToListAsync();

            if (!reviews.Any())
                return 0.0; 

            return reviews.Average(r => r.Rate); 
        }

    }
}
