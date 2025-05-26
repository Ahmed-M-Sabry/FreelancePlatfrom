using FreelancePlatfrom.Data.Entities.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IReportService
    {
        Task AddReportAsync(Reports report);
        Task UpdateReportAsync(Reports reports);
        Task DeleteReportAsync(Reports reports);
        Task<Reports> GetReportByIdAsync(int id);
        Task<List<Reports>> GetAllByClientIdAsync(string clientId);
        Task<List<Reports>> GetAllAsync();

    }
}
