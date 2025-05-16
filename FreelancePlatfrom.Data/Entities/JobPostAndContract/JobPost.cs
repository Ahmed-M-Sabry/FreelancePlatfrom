using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.JobPostAndContract
{
    /// <summary>
    /// Represents a job post created by a client.
    /// </summary>
    public class JobPost
    {
        private int _id;
        private string _title;
        private string _description;
        private decimal _price;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

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

        public string Status { get; set; } // Completed, Uncompleted

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<JobPostSkill> JobPostSkills { get; set; }
        public List<ApplyTask> ApplyTasks { get; set; }
        public List<FavJobPost> FavJobPosts { get; set; } = new List<FavJobPost>();

    }
}
