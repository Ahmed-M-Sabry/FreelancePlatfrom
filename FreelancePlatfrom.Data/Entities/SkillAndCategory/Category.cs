using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.SkillAndCategory
{
    /// <summary>
    /// Represents a category for job posts.
    /// </summary>
    public class Category
    {
        private int _id;
        private string _name;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public bool IsDeleted { get; set; }

        public List<SkillCategory> SkillCategories { get; set; }
        public List<JobPost> JobPosts { get; set; }
    }
}
