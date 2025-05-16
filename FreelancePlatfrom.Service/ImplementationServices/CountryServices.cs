using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class CountryServices : ICountryServices
    {
        private readonly ICountryRepository _countryRepository;
        public CountryServices(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> GetCountryByIdAsync(string id)
        {
            return await _countryRepository.GetCountryByIdAsync(id);
        }
    }

}
