using FluentValidation;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Validator
{
    public class CreateApplyTaskValidator : AbstractValidator<CreateApplyTaskCommand>
    {
        public CreateApplyTaskValidator()
        {
            RuleFor(x => x.OfferDescription)
                .NotEmpty()
                .MinimumLength(20)
                .WithMessage("Offer description must be at least 20 characters long.");

            RuleFor(x => x.DeliveryDate)
                .NotNull()
                .GreaterThan(x => x.OrderDate)
                .WithMessage("Delivery date must be after the order date.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total amount cannot be negative.");

            RuleFor(x => x.JobPostId)
                .GreaterThan(0)
                .WithMessage("JobPost Can't be less Than 0");

            RuleFor(x => x.ClientId)
                .NotEmpty();
        }
    }
}
