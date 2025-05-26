using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.JobPostAndContract
{
    /// <summary>
    /// Represents a contract between a client and a freelancer for a job post.
    /// </summary>
    public class Contracts
    {
        private int _id;
        private string _termsAndConditions;
        private decimal _price;
        private DateTime _contractDate;
        private DateTime _startDate;
        private DateTime _endDate;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string TermsAndConditions
        {
            get => _termsAndConditions;
            set => _termsAndConditions = value ?? throw new ArgumentNullException(nameof(TermsAndConditions));
        }

        public decimal Price
        {
            get => _price;
            set => _price = value >= 0 ? value : throw new ArgumentException("Price cannot be negative.");
        }

        public DateTime ContractDate
        {
            get => _contractDate;
            set => _contractDate = value;
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value >= _startDate ? value : throw new ArgumentException("EndDate must be after StartDate.");
        }

        public string Status { get; set; } // Accepted, Rejected, Pending

        public int ApplyTaskId { get; set; }
        public ApplyTask ApplyTask { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public string FreelancerId { get; set; }
        public ApplicationUser Freelancer { get; set; }

        public Review Review { get; set; }
    }
}
