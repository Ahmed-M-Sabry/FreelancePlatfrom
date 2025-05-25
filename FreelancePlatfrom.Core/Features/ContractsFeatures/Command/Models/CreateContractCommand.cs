using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models
{
    public class CreateContractCommand : IRequest<ApiResponse<string>>
    {
        public string TermsAndConditions { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ApplyTaskId { get; set; }
        public string FreelancerId { get; set; }
    }
}
