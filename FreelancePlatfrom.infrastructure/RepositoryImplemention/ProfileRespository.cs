using FreelancePlatfrom.Data.Entities;
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
    public class ProfileRespository : GenericRepositoryAsync<Portfolio>, IProfileRespository
    {
        private readonly ApplicationDbContext _context;
        public ProfileRespository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        // You can add any specific methods for ProfileRespository here if needed

    }
}
