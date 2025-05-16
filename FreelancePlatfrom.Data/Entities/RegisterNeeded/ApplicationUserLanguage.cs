using FreelancePlatfrom.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.RegisterNeeded
{
    /// <summary>
    /// Represents the many-to-many relationship between ApplicationUser and Language.
    /// </summary>
    public class ApplicationUserLanguage
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
