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
 /// Represents a task application by a freelancer for a job post.
 /// </summary>
    public class ApplyTask
    {
        private int _id;
        private string _offerDescription;
        private DateTime _orderDate;
        private decimal _totalAmount;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string OfferDescription
        {
            get => _offerDescription;
            set => _offerDescription = value ?? throw new ArgumentNullException(nameof(OfferDescription));
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set => _orderDate = value;
        }

        public DateTime? DeliveryDate { get; set; }

        public string? Status { get; set; } // Accepted, Rejected, Pending, Complete, In progress, Cancel

        public decimal TotalAmount
        {
            get => _totalAmount;
            set => _totalAmount = value >= 0 ? value : throw new ArgumentException("TotalAmount cannot be negative.");
        }

        public bool IsDeleted { get; set; }

        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }

        public string FreelancerId { get; set; }
        public ApplicationUser Freelancer { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }


    }
}
