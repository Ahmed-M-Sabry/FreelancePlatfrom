using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.JobPostAndContract
{
    /// <summary>
    /// Represents the many-to-many relationship between JobPost and Skill.
    /// </summary>
    public class JobPostSkill
    {
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }
    }
}
