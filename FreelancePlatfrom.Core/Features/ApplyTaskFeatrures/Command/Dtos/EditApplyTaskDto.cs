using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Dtos
{
    public class EditApplyTaskDto
    {
        public int Id { get; set; }

        public string OfferDescription { get; set; }

        public DateTime DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
