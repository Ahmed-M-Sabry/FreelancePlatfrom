using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Models
{
    public class GetApplyTaskByIdQuery : IRequest<ApiResponse<GetApplyTaskByIdDto>>
    {
        public int Id { get; set; }
        public GetApplyTaskByIdQuery(int id)
        {
            Id = id;
        }
    }
}
