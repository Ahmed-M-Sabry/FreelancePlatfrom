using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models
{
    public class CreateCategoryCommand : IRequest<ApiResponse<string>>
    {
        public string CategoryName { get; set; }
        public CreateCategoryCommand(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
