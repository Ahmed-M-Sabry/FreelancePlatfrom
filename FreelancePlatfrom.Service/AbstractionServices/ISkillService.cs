using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface ISkillService
    {
        Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds);
    }
}
