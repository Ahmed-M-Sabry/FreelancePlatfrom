using FreelancePlatfrom.Core.Base;
using MediatR;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models
{
    public class EditReviewCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int TaskCompletesPercentage { get; set; }
        public string? Comments { get; set; }
    }
}
