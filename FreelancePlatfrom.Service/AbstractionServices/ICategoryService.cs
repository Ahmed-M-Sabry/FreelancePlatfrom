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
    }
}
