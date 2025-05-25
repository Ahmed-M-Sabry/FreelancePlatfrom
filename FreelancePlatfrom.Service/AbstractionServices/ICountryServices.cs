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
        Task<Country> GetCountryByNameAsync(string name);

        Task UpdateCountry(Country country);
        Task AddCountry(Country country);
        Task DeleteCountry(string id);


        Task<List<Country>> GetAllCountriesForAdmin();
        Task<List<Country>> GetAllCountriesForUser();
    }
}
