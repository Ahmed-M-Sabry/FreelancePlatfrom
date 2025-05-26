using FreelancePlatfrom.Data.Entities;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IGenericRepositoryAsync<Portfolio> _repo;

        public PortfolioService(IGenericRepositoryAsync<Portfolio> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            await _repo.AddAsync(portfolio);
        }

        public async Task DeleteAsync(Portfolio portfolio)
        {
             await _repo.DeleteAsync(portfolio);
        }

        public async Task<Portfolio?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            await _repo.UpdateAsync(portfolio);
        }

    }

}
