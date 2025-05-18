using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Dtos
{
    public class CreateJobPostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? DurationTime { get; set; }
        public int CategoryId { get; set; }
        public List<int> SkillIds { get; set; } = new();
    }
}
