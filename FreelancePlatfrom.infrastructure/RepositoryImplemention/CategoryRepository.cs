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

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            return await _context.Categories.Where(i=>i.Id == id && !i.IsDeleted)
                .Select(i => i.Name)
                .FirstOrDefaultAsync() ?? "Not Found";
        }
    }
}
