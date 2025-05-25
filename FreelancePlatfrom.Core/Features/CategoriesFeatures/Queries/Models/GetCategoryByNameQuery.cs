using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Models
{
    public class GetCategoryByNameQuery : IRequest<ApiResponse<GetCategoryByNameResponse>>
    {
        public string Name { get; set; }
        public GetCategoryByNameQuery(string name)
        {
            Name = name;
        }
    }
}
