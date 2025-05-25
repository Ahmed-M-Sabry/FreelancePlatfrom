using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task CreateNewCategory(Category category)
        {
            return _categoryRepository.AddAsync(category);
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategory(id);
        }

        public async Task<List<Category>> GetAllCategoriesForAdminAsync()
        {
            return await _categoryRepository.GetAllCategoriesForAdminAsync();
        }

        public async Task<List<Category>> GetAllCategoriesForUserAsync()
        {
            return await _categoryRepository.GetAllCategoriesForUserAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _categoryRepository.GetCategoryByNameAsync(name);
        }

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryNameByIdAsync(id);
        }

        public async Task<Category> RestoreCategory(int id)
        {
            return await _categoryRepository.RestoreCategory(id);
        }

        public Task UpdatedCategory(Category category)
        {
            return _categoryRepository.UpdateAsync(category);
        }
    }
}
