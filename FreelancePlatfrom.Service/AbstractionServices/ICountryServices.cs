using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface ICountryServices  
    {
        Task<Country> GetCountryByIdAsync(string id);
    }
}
