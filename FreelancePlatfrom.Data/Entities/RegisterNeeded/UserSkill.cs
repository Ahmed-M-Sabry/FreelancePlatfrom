using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.RegisterNeeded
{
    /// <summary>
    /// Represents the many-to-many relationship between ApplicationUser and Skill.
    /// </summary>
    public class UserSkill
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
