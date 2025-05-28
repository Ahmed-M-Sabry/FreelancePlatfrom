using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface ILanguagesService
    {
        Task<List<string>> GetValidLanguageNamesAsync(List<string> languageIds);
        Task<Language> GetLanguageByIdAsync(string id);
        Task<Language> GetLanguageByNameAsync(string value);
        Task CreateAsync(Language language);
        Task UpdateAsync(Language language);
        Task<List<Language>> GetAllLanguagesAsync();
        Task<List<Language>> GetAllUserLanguagesAsync();

        Task<List<string>> GetUserLanguageIdsAsync(string userId);
        Task AddUserLanguagesAsync(List<ApplicationUserLanguage> userLanguages);
        Task RemoveUserLanguagesAsync(string userId, List<string> languageIds);
    }
}
