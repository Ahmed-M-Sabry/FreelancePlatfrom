using FreelancePlatfrom.Data.Entities.Report;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class ReportService : IReportService
    {
        private readonly IRepostRepostiory _repostRepostiory;
        public ReportService(IRepostRepostiory repostRepostiory)
        {
            _repostRepostiory = repostRepostiory;
        }
        public async Task AddReportAsync(Reports report)
        {
            await _repostRepostiory.AddAsync(report);
        }
        public async Task UpdateReportAsync(Reports reports)
        {
            await _repostRepostiory.UpdateAsync(reports);
        }
        public async Task DeleteReportAsync(Reports reports)
        {
            await _repostRepostiory.DeleteAsync(reports);
        }
        public async Task<Reports> GetReportByIdAsync(int id)
        {
            return await _repostRepostiory.GetByIdAsync(id);
        }

        public async Task<List<Reports>> GetAllByClientIdAsync(string clientId)
        {
            return await _repostRepostiory.GetAllByClientIdAsync(clientId);
        }

        public async Task<List<Reports>> GetAllAsync()
        {
            return await _repostRepostiory.GetAllAsync();
        }
    }
}
