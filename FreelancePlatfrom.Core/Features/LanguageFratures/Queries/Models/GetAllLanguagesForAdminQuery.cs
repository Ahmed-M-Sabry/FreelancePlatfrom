using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
using MediatR;
using System.Collections.Generic;

namespace FreelancePlatfrom.Core.Features.LanguagesFeatures.Query.GetAllLanguagesForAdmin
{
    public class GetAllLanguagesForAdminQuery : IRequest<ApiResponse<List<GetAllLanguagesForAdminResponse>>> 
    {
        
    }
}
