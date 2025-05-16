using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.SkillAndCategory
{
    /// <summary>
    /// Represents the many-to-many relationship between Skill and Category.
    /// </summary>
    public class SkillCategory
    {
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
