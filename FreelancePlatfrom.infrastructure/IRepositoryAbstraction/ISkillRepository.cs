using FreelancePlatfrom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface ISkillRepository
    {

        Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds);

    }
}
