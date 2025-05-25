using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models
{
    public class RestoreCategoryCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public RestoreCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
