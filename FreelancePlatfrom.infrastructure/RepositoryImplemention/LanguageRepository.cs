using FreelancePlatfrom.Data.Entities.RegisterNeeded;
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
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
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
