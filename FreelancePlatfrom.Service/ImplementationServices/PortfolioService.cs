using FreelancePlatfrom.Data.Entities;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
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
        private readonly IProfileRespository _profileRespository;

        public PortfolioService(IProfileRespository profileRespository)
        {
            _profileRespository = profileRespository;
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            await _profileRespository.AddAsync(portfolio);
        }

        public async Task DeleteAsync(Portfolio portfolio)
        {
             await _profileRespository.DeleteAsync(portfolio);
        }

        public async Task<List<Portfolio>> GetByFreelancerIdAsync(string freelancerId)
        {
            return await _profileRespository.GetByFreelancerIdAsync(freelancerId);
        }

        public async Task<Portfolio?> GetByIdAsync(int id)
        {
            return await _profileRespository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            await _profileRespository.UpdateAsync(portfolio);
        }

    }

}
