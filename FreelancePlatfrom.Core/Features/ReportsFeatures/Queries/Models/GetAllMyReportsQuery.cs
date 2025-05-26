using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Results;
using FreelancePlatfrom.Data.Entities.Report;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Models
{
    public class GetAllMyReportsQuery : IRequest<ApiResponse<List<GetReportResponse>>>
    {
    }
}
