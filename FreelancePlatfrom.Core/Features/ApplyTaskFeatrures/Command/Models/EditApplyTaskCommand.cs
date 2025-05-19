using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models
{
    public class EditApplyTaskCommand : IRequest<ApiResponse<string>>
    {
        public  int Id { get; set; }
        private string _offerDescription;
        private DateTime _orderDate;
        private decimal _totalAmount;

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

        public string FreelancerId { get; set; }

        public string ClientId { get; set; }

        public static EditApplyTaskCommand FromDto(EditApplyTaskDto dto)
        {
            return new EditApplyTaskCommand
            {
                Id = dto.Id,
                OfferDescription = dto.OfferDescription,
                DeliveryDate = dto.DeliveryDate,
                TotalAmount = dto.TotalAmount,

            };
        }
    }
}
