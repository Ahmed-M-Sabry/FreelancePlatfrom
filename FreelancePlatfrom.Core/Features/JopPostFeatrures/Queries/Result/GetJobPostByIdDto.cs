using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Result
{
    public class GetJobPostByIdDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        private string _title;
        private string _description;
        private decimal _price;

        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException(nameof(Title));
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(Description));
        }

        public decimal Price
        {
            get => _price;
            set => _price = value >= 0 ? value : throw new ArgumentException("Price cannot be negative.");
        }

        public DateTime? DurationTime { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public List<int> SkillIds { get; set; } = new List<int>();
        public List<string> SkillNames { get; set; }
    }
}
