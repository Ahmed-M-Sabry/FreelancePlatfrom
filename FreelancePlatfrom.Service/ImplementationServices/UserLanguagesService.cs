using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class UserLanguagesService : IUserLanguagesService
    {
        private readonly IUserLanguagesRepository _languageRepository;
        public UserLanguagesService(IUserLanguagesRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }
        public async Task<List<string>> GetValidLanguageNamesAsync(List<string> languageIds)
        {
            return await _languageRepository.GetValidLanguageNamesAsync(languageIds);
        }
    }
}
