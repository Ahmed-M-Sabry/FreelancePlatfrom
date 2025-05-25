using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface ICategoryRepository : IGenericRepositoryAsync<Category>
    {
        Task<string> GetCategoryNameByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task<Category> DeleteCategory(int id);
        Task<Category> RestoreCategory(int id);

        Task<List<Category>> GetAllCategoriesForUserAsync();
        Task<List<Category>> GetAllCategoriesForAdminAsync();
    }

}
