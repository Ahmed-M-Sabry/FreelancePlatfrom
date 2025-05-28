using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models
{
    public class EditFreelancerGeneralInfoCommand : IRequest<ApiResponse<string>>
    {
        public int? Age { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PortfolioUrl { get; set; }
    }
}
