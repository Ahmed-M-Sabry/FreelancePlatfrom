using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.FavoritesTables
{
    /// <summary>
    /// Represents a favorite job post saved by a freelancer.
    /// </summary>
    public class FavJobPost
    {
        private int _id;
        private string _freelancerId;
        private int _jobPostId;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string FreelancerId
        {
            get => _freelancerId;
            set => _freelancerId = value ?? throw new ArgumentNullException(nameof(FreelancerId));
        }

        public ApplicationUser Freelancer { get; set; }

        public int JobPostId
        {
            get => _jobPostId;
            set => _jobPostId = value;
        }

        public JobPost JobPost { get; set; }


    }
}
