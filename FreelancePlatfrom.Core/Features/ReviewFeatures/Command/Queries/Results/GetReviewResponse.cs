using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Results
{
    public class GetReviewResponse
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int TaskCompletesPercentage { get; set; }
        public string? Comments { get; set; }
        public DateTime RateDate { get; set; }

        public string ClientId { get; set; }
        public string ClientName { get; set; }

        public string FreelancerId { get; set; }
        public string FreelancerName { get; set; }
    }

}
