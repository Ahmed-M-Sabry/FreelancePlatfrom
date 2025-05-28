using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface ILanguagesRepository : IGenericRepositoryAsync<Language>
    {
        Task<List<string>> GetValidLanguageNamesAsync(List<string> languageIds);
        Task<Language> GetLanguageByIdAsync(string id);
        Task<Language> GetLanguageByNameAsync(string value);
        Task<List<Language>> GetAllLanguagesAsync();
        Task<List<Language>> GetAllUserLanguagesAsync();


        Task<List<string>> GetUserLanguageIdsAsync(string userId);
        Task AddUserLanguagesAsync(List<ApplicationUserLanguage> userLanguages);
        Task RemoveUserLanguagesAsync(string userId, List<string> languageIds);
    }
}
