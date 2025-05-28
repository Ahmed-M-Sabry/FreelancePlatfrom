using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models
{
    public class EditFreelancerProfessionalInfoCommand : IRequest<ApiResponse<string>>
    {
        public string? YourTitle { get; set; }
        public string? Description { get; set; }
        public string? Education { get; set; }
        public string? Experience { get; set; }
        public decimal? HourlyRate { get; set; }
    }

}
