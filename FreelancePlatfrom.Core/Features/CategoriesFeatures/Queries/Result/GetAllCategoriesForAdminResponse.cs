using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Result
{
    public class GetAllCategoriesForAdminResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

    }
}
