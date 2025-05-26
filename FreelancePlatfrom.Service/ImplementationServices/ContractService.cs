using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public Task AddContract(Contracts contracts)
        {
            return _contractRepository.AddAsync(contracts);
        }

        public Task DeleteContract(Contracts contracts)
        {
            return _contractRepository.DeleteAsync(contracts);
        }

        public async Task<Contracts> GetContractByApplyTaskId(int ApplyTaskId)
        {
            return await _contractRepository.GetContractByApplyTaskId(ApplyTaskId);
        }

        public async Task<Contracts> GetContractById(int ContractId)
        {
            return await _contractRepository.GetContractById(ContractId);
        }

        public async Task<Contracts> GetContractWithIncludes(int id, string userId)
        {
            return await _contractRepository.GetContractWithIncludes(id , userId);
        }

        public async Task<List<Contracts>> GetMyContractsAsync(string userId)
        {
            return await _contractRepository.GetMyContractsAsync(userId);
        }

        public Task UpdateContract(Contracts contract)
        {
            return _contractRepository.UpdateAsync(contract);
        }

    }
}
