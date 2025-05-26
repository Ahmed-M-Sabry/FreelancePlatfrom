using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IContractRepository : IGenericRepositoryAsync<Contracts>
    {
        Task<Contracts> GetContractByApplyTaskId(int ApplyTaskId);
        Task<Contracts> GetContractById(int ContractId);
        Task<Contracts> GetContractWithIncludes(int id , string userId);
        Task<List<Contracts>> GetMyContractsAsync(string userId);
        Task<Contracts> GetContractWithAccpeted(int id , string userId , string FreelancerId);

    }
}
