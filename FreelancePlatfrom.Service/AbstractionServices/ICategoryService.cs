using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(int id);
        Task<string> GetCategoryNameByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task CreateNewCategory(Category category);
        Task UpdatedCategory(Category category);
        Task<Category> DeleteCategoryAsync(int id);
        Task<Category> RestoreCategory(int id);
        Task<List<Category>> GetAllCategoriesForUserAsync();
        Task<List<Category>> GetAllCategoriesForAdminAsync();

    }
}
