using FreelancePlatfrom.Data.Entities.FavoritesTables;
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
    public class FavoritesFreelancerRepository :GenericRepositoryAsync<FavoritesFreelancer>  ,IFavoritesFreelancerRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoritesFreelancerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FavoritesFreelancer>> GetAllFreelancersFavoritedByClient(string clientId)
        {
            return await _context.favoritesFreelancers
                .Where(f => f.ClientId == clientId)
                .Include(f => f.Freelancer) // Assuming you want to include the Freelancer details
                .ToListAsync();
        }

        public async Task<FavoritesFreelancer> GetFavoritesFreelancerById(string freelancerId)
        {
            return await _context.favoritesFreelancers
                .FirstOrDefaultAsync(f => f.FreelancerId == freelancerId);
        }
        public async Task<bool> IsFreelancerFavorited(string clientId, string freelancerId)
        {
            return await _context.favoritesFreelancers.
                AnyAsync(f => f.ClientId == clientId && f.FreelancerId == freelancerId);
        }

    }
}
