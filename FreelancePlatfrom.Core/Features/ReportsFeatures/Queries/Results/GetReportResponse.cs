using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Results
{
    public class GetReportResponse
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; }

        public string ClientId { get; set; }
        public string ClientName { get; set; }

        public string FreelancerId { get; set; }
        public string FreelancerName { get; set; }
    }
}
