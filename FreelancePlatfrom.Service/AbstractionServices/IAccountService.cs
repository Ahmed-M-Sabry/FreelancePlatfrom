using FreelancePlatfrom.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IAccountService
    {
        Task<ApplicationUser> GetApplicationFreelancerByIdAsync(string userId);
        Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);

        Task UpdateUserAsync(ApplicationUser user);
    }
}
