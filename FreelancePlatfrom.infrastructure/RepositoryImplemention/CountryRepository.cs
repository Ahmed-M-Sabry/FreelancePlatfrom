using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.RepositoryImplemention
{
    public class CountryRepository : GenericRepositoryAsync<Country> , ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a country by its ID.
        /// </summary>
        /// <param name="id">The ID of the country.</param>
        /// <returns>The country with the specified ID.</returns>
        public async Task<Country> GetCountryByIdAsync(string id)
        {
            return await _context.countries.FirstOrDefaultAsync(i=>i.Id == id);
        }
    }
}
