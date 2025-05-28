using FreelancePlatfrom.Data.Entities.Identity;
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
    public class AccountRepository :GenericRepositoryAsync<ApplicationUser> ,IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> GetApplicationFreelancerByIdAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.country)
                .Include(u => u.UserLanguages)
                    .ThenInclude(ul => ul.Language)
                .Include(u => u.UserSkills)
                    .ThenInclude(us => us.Skill)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.country)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
