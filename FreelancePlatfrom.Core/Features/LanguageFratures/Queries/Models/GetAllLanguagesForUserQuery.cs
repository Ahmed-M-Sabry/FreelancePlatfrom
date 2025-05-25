using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
using FreelancePlatfrom.Core.Features.LanguagesFeatures.Query.GetAllLanguagesForAdmin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Models
{
    public class GetAllLanguagesForUserQuery : IRequest<ApiResponse<List<GetAllLanguagesForUserResponse>>>
    {

    }
}
