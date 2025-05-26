using FreelancePlatfrom.Data.Entities.JobPostAndContract;
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
    public class ContractRepository : GenericRepositoryAsync<Contracts> , IContractRepository
    {
        private readonly ApplicationDbContext _context;
        public ContractRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Contracts> GetContractByApplyTaskId(int ApplyTaskId)
        {
            return await _context.Contracts.FirstOrDefaultAsync(c => c.ApplyTaskId == ApplyTaskId);
        }

        public async Task<Contracts> GetContractById(int ContractId)
        {
            return await _context.Contracts.FirstOrDefaultAsync(c => c.Id == ContractId);
        }
        public async Task<Contracts> GetContractWithIncludes(int id , string userId)
        {
            return await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Freelancer)
                .Include(c => c.ApplyTask)
                    .ThenInclude(a => a.JobPost)
                .FirstOrDefaultAsync(c => c.Id == id && ( c.ClientId == userId || c.FreelancerId == userId));
        }
        public async Task<List<Contracts>> GetMyContractsAsync(string userId)
        {
            return await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Freelancer)
                .Include(c => c.ApplyTask)
                    .ThenInclude(a => a.JobPost)
                .Where(c => c.ClientId == userId || c.FreelancerId == userId)
                .ToListAsync();
        }


    }
}
