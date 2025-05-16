using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface ICountryRepository : IGenericRepositoryAsync<Country>
    {
        /// <summary>
        /// Retrieves a country by its ID.
        /// </summary>
        /// <param name="id">The ID of the country.</param>
        /// <returns>The country with the specified ID.</returns>
        Task<Country> GetCountryByIdAsync(string id);
    }
}
