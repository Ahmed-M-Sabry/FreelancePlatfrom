using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Results
{
    public class GetMyContractsResponse
    {
        public int Id { get; set; }
        public string TermsAndConditions { get; set; }
        public decimal Price { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public string ClientFullName { get; set; }
        public string ClientId { get; set; }
        public string FreelancerFullName { get; set; }
        public string FreelancerId { get; set; }

        public string JobPostTitle { get; set; }
        public string JobPostDescription { get; set; }
    }
}
