using FreelancePlatfrom.Data.Entities.Report;
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
    public class RepostRepostiory : GenericRepositoryAsync<Reports> , IRepostRepostiory
    {
        private readonly ApplicationDbContext _context;
        public RepostRepostiory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Reports>> GetAllByClientIdAsync(string clientId)
        {
            return await _context.Reports
                .Include(r => r.Client)
                .Include(r => r.Freelancer)
                .Where(x => x.ClientId == clientId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Reports>> GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.Client)
                .Include(r => r.Freelancer)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Reports> GetByIdAsync(int id)
        {
            return await _context.Reports
                .Include(r => r.Client)
                .Include(r => r.Freelancer)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

        }
    }
}
