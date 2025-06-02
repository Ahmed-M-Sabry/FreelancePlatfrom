using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IAccountRepository : IGenericRepositoryAsync<ApplicationUser>
    {
        Task<ApplicationUser> GetApplicationFreelancerByIdAsync(string userId);
        Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
        Task<List<ApplicationUser>> SearchFreelancersAsync(string keyword);

    }
}
