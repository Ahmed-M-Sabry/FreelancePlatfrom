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

        public Task AddCountry(Country country)
        {
            return _countryRepository.AddAsync(country);
        }

        public async Task DeleteCountry(string id)
        {
            var country = await _countryRepository.GetCountryByIdAsync(id);
            if(country != null)
            {
                country.IsDeleted = true;
                _countryRepository.SaveChangesAsync();
            }
        }

        public async Task<List<Country>> GetAllCountriesForAdmin()
        {
            return await _countryRepository.GetAllCountriesForAdmin();
        }

        public async Task<List<Country>> GetAllCountriesForUser()
        {
            return await _countryRepository.GetAllCountriesForUser();

        }

        public async Task<Country> GetCountryByIdAsync(string id)
        {
            return await _countryRepository.GetCountryByIdAsync(id);
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            return await _countryRepository.GetCountryByNameAsync(name);
        }

        public Task UpdateCountry(Country country)
        {
            return _countryRepository.UpdateAsync(country);
        }
    }

}
