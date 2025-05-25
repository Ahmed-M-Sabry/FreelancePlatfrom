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
    public class GetAllCategoriesForAdminQuery : IRequest<ApiResponse<List<GetAllCategoriesForAdminResponse>>>
    {

    }
}
