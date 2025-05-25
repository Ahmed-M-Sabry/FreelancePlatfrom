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
    public class GetLanguageByNameQuery : IRequest<ApiResponse<GetLanguageByNameResponse>>
    {
        public string Value { get; set; }

        public GetLanguageByNameQuery(string value)
        {
            Value = value;
        }
    }
}
