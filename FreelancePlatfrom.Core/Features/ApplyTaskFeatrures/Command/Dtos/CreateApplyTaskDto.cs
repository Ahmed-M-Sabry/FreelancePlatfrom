using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Dtos
{
    public class CreateApplyTaskDto
    {

        private string _offerDescription;
        private decimal _totalAmount;

        public string OfferDescription
        {
            get => _offerDescription;
            set => _offerDescription = value ?? throw new ArgumentNullException(nameof(OfferDescription));
        }
        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set => _totalAmount = value >= 0 ? value : throw new ArgumentException("TotalAmount cannot be negative.");
        }


        public int JobPostId { get; set; }

        public string ClientId { get; set; }
    }
}
