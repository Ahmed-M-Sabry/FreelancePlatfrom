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
    }
}
