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
    public class GetCountryByIdQuery : IRequest<ApiResponse<GetCountryByIdResponse>>
    {
        public string Id { get; set; }
        public GetCountryByIdQuery(string id)
        {
            Id = id;
        }
    }
}
