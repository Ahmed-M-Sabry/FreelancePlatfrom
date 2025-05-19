using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result
{
    public class GetApplyTaskByIdDto
    {
        public int Id { get; set; }
        public int JobPostId { get; set; }
        public string JobPostName { get; set; }
        public string JobPostDescription { get; set; }
        public string? Status { get; set; }
        public string? FreeLanceId { get; set; }
        public string? FreeLancerName { get; set; }
        public string? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? OfferDescription { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
