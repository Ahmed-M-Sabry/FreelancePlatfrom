using FreelancePlatfrom.Data.Entities;
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
    public class ProfileRespository : GenericRepositoryAsync<Portfolio>, IProfileRespository
    {
        private readonly ApplicationDbContext _context;
        public ProfileRespository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        // You can add any specific methods for ProfileRespository here if needed
        public async Task<List<Portfolio>> GetByFreelancerIdAsync(string freelancerId)
        {
            return await _context.Portfolio
                .Include(p => p.ApplicationUser)
                .Where(p => p.UserId == freelancerId)
                .ToListAsync();
        }
        public async Task<Portfolio> GetByIdAsync(int id)
        {
            return await _context.Portfolio
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
