using FreelancePlatfrom.Data.Entities.Report;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IRepostRepostiory : IGenericRepositoryAsync<Reports>
    {
        Task<List<Reports>> GetAllByClientIdAsync(string clientId);
        Task<List<Reports>> GetAllAsync();
        Task<Reports> GetByIdAsync(int id);
    }
}
