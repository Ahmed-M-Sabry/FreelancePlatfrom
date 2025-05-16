using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Shared
{
    public class ApplicationRoles
    {
        /// <summary>
        /// The role name for a standard user.
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// The role name for an administrator.
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// The role name for a freelancer.
        /// </summary>
        public const string Freelancer = "Freelancer";

        /// <summary>
        /// Gets a list of all role names.
        /// </summary>
        public static List<string> AllRoles => new List<string> { User, Admin, Freelancer };
    }
}
