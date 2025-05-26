using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models
{
    public class DeleteReviewCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public DeleteReviewCommand(int id)
        {
            Id = id;
            
        }
    }
}
