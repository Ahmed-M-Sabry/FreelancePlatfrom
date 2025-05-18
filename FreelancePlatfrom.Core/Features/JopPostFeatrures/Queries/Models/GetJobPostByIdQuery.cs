using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Models
{
    public class GetJobPostByIdQuery : IRequest<ApiResponse<GetJobPostByIdDto>>
    {
        public int Id { get; set; }

        public GetJobPostByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
