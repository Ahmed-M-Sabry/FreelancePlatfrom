using FreelancePlatfrom.Data.Entities.SkillAndCategory;
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
    public class CategoryRepository : GenericRepositoryAsync<Category> , ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var oldData = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
            oldData.IsDeleted = true;
            await _context.SaveChangesAsync();
            return oldData;
        }

        public async Task<List<Category>> GetAllCategoriesForAdminAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesForUserAsync()
        {
            return await _context.Categories.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
        }

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            return await _context.Categories.Where(i=>i.Id == id && !i.IsDeleted)
                .Select(i => i.Name)
                .FirstOrDefaultAsync() ?? "Not Found";
        }

        public async Task<Category> RestoreCategory(int id)
        {
            var oldData = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
            oldData.IsDeleted = false;
            await _context.SaveChangesAsync();
            return oldData;
        }
    }
}
