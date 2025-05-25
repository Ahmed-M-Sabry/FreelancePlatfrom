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
    public class LanguagesRepository : GenericRepositoryAsync<Language>, ILanguagesRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguagesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Language>> GetAllLanguagesAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<List<Language>> GetAllUserLanguagesAsync()
        {
            return await _context.Languages
                .Where(l => !l.IsDeleted)
                .ToListAsync();
        }
        public async Task<Language> GetLanguageByIdAsync(string id)
        {
            return await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Language> GetLanguageByNameAsync(string value)
        {
            return await _context.Languages
                .FirstOrDefaultAsync(l => l.Value.ToLower() == value.ToLower());
        }

        public async Task<List<string>> GetValidLanguageNamesAsync(List<string> languageIds)
        {
            var validLanguageNames = await _context.Languages
                .Where(l => languageIds.Contains(l.Id) && !l.IsDeleted)
                .Select(l => l.Value)
                .ToListAsync();

            return validLanguageNames;
        }

    }
}
