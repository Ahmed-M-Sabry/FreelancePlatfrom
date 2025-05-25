using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class LanguagesService : ILanguagesService
    {
        private readonly ILanguagesRepository _languageRepository;
        public LanguagesService(ILanguagesRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public Task CreateAsync(Language language)
        {
            return _languageRepository.AddAsync(language);
        }

        public async Task<List<Language>> GetAllLanguagesAsync()
        {
            return await _languageRepository.GetAllLanguagesAsync();
        }

        public async Task<List<Language>> GetAllUserLanguagesAsync()
        {
            return await _languageRepository.GetAllUserLanguagesAsync();
        }

        public async Task<Language> GetLanguageByIdAsync(string id)
        {
            return await _languageRepository.GetLanguageByIdAsync(id);
        }

        public async Task<Language> GetLanguageByNameAsync(string value)
        {
            return await _languageRepository.GetLanguageByNameAsync(value);
        }

        public async Task<List<string>> GetValidLanguageNamesAsync(List<string> languageIds)
        {
            return await _languageRepository.GetValidLanguageNamesAsync(languageIds);
        }

        public Task UpdateAsync(Language language)
        {
            return _languageRepository.UpdateAsync(language);   
        }
    }
}
