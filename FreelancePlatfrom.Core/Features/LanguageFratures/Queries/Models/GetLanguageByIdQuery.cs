using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Models
{
    public class GetLanguageByIdQuery : IRequest<ApiResponse<GetLanguageByIdResponse>>
    {
        public string Id { get; set; }
        public GetLanguageByIdQuery(string id)
        {
            Id = id;
        }

    }
}
