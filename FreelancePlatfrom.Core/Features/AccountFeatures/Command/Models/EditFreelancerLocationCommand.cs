using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models
{
    public class EditFreelancerLocationCommand : IRequest<ApiResponse<string>>
    {
        public string? State { get; set; }
        public string? Address { get; set; }
        public string? CountryId { get; set; }
        public int? Zip { get; set; }
    }

}
