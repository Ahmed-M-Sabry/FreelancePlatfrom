using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.Rating
{
    /// <summary>
    /// Represents a review given by a client to a freelancer for a task.
    /// </summary>
    public class Review
    {
        private int _id;
        private int _rate;
        private int _taskCompletesPercentage;
        private DateTime _rateDate;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int Rate
        {
            get => _rate;
            set => _rate = value >= 1 && value <= 5 ? value : throw new ArgumentException("Rate must be between 1 and 5.");
        }

        public int TaskCompletesPercentage
        {
            get => _taskCompletesPercentage;
            set => _taskCompletesPercentage = value >= 0 && value <= 100 ? value : throw new ArgumentException("TaskCompletesPercentage must be between 0 and 100.");
        }

        public string? Comments { get; set; }

        public DateTime RateDate
        {
            get => _rateDate;
            set => _rateDate = value;
        }

        public bool IsDeleted { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public string FreelancerId { get; set; }
        public ApplicationUser Freelancer { get; set; }

        public int ApplyTaskId { get; set; }
        public ApplyTask ApplyTask { get; set; }
    }
}
