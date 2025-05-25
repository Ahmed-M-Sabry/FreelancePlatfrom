using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Models
{
    public class GetAllCountryForUserQuery : IRequest<ApiResponse<List<GetAllCountryForUserResponse>>>
    {

    }
}
