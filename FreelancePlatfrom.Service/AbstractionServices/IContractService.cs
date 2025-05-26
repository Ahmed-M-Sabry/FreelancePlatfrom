using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IContractService
    {
        Task AddContract(Contracts contracts);
        Task<Contracts> GetContractByApplyTaskId(int ApplyTaskId);
        Task<Contracts> GetContractById(int ContractId);
        Task UpdateContract(Contracts contract);
        Task DeleteContract(Contracts contracts);
        Task<Contracts> GetContractWithIncludes(int id, string userId);
        Task<List<Contracts>> GetMyContractsAsync(string userId);


    }
}
