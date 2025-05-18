using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IUserLanguagesRepository
    {
        Task<List<string>> GetValidLanguageNamesAsync(List<string> languageIds);

    }
}
