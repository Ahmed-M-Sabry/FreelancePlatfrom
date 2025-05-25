using FluentValidation;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Validator
{
    public class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
    {
        public CreateContractCommandValidator()
        {
            RuleFor(x => x.TermsAndConditions)
                .NotEmpty().WithMessage("Terms and Conditions are required.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive value.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");

            RuleFor(x => x.FreelancerId)
                .NotEmpty().WithMessage("Freelancer ID is required.");

            RuleFor(x => x.ApplyTaskId)
                .GreaterThan(0).WithMessage("ApplyTask ID is required and must be a valid number.");
        }
    }
}
