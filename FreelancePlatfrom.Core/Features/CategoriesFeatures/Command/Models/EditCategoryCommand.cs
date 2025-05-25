using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models
{
    public class EditCategoryCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
