using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Response
{
    public class PortfolioByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FreelancerName { get; set; }
        public string UserId { get; set; }
        public string? Media { get; set; }
        public string? Url { get; set; }
        public DateTime? ProjectDate { get; set; }
    }
}
