using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.infrastructure.RepositoryImplemention;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ApplicationUser> GetApplicationFreelancerByIdAsync(string userId)
        {
            return await _accountRepository.GetApplicationFreelancerByIdAsync(userId);
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
        {
            return await _accountRepository.GetApplicationUserByIdAsync(userId);
        }

        public Task UpdateUserAsync(ApplicationUser user)
        {
            return _accountRepository.UpdateAsync(user);
        }
    }
}
