using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Response
{
    public class FreelancerPortfolioResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FreelancerName { get; set; }
        public string UserId { get; set; }
    }
}
