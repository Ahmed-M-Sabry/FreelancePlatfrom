using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.RepositoryImplemention
{
    public class ContractRepository : GenericRepositoryAsync<Contracts> , IContractRepository
    {
        private readonly ApplicationDbContext _context;
        public ContractRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
