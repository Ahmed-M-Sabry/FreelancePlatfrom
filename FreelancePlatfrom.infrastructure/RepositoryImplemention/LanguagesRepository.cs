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
        public async Task<List<string>> GetValidLanguageIdsAsync(List<string> languageIds)
        {
            return await _context.Languages
                .Where(l => languageIds.Contains(l.Id) && !l.IsDeleted)
                .Select(l => l.Id)
                .ToListAsync();
        }
        public async Task<List<string>> GetUserLanguageIdsAsync(string userId)
        {
            return await _context.ApplicationUserLanguages
                .Where(ul => ul.ApplicationUserId == userId)
                .Select(ul => ul.LanguageId)
                .ToListAsync();
        }
        public async Task AddUserLanguagesAsync(List<ApplicationUserLanguage> userLanguages)
        {
            await _context.ApplicationUserLanguages.AddRangeAsync(userLanguages);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserLanguagesAsync(string userId, List<string> languageIds)
        {
            var toRemove = await _context.ApplicationUserLanguages
                .Where(ul => ul.ApplicationUserId == userId && languageIds.Contains(ul.LanguageId))
                .ToListAsync();

            if (toRemove.Any())
            {
                _context.ApplicationUserLanguages.RemoveRange(toRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
