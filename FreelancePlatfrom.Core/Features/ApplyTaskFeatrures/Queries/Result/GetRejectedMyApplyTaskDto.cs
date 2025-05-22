using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result
{
    public class GetRejectedMyApplyTaskDto
    {
        public int Id { get; set; }
        public int JobPostId { get; set; }
        public string JobPostName { get; set; }
        public string JobPostDescription { get; set; }
        public string? Status { get; set; }
        public string? ClientId { get; set; }
        public string? ClientName { get; set; }
    }
}
