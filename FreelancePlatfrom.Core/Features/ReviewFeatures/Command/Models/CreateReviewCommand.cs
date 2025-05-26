using FreelancePlatfrom.Core.Base;
using MediatR;
using System;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models
{
    public class CreateReviewCommand : IRequest<ApiResponse<string>>
    {
        public int Rate { get; set; }
        public int TaskCompletesPercentage { get; set; }
        public string? Comments { get; set; }
        public int ContractId { get; set; }
        public string FreelancerId { get; set; }
    }
}
