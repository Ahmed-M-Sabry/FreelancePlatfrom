using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.SkillAndCategory
{
    /// <summary>
    /// Represents a skill that can be associated with users or job posts.
    /// </summary>
    public class Skill
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

        public List<UserSkill> UserSkills { get; set; }
        public List<SkillCategory> SkillCategories { get; set; }
        public List<JobPostSkill> JobPostSkills { get; set; }
    }
}
