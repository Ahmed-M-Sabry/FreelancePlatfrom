using FluentValidation;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Validator
{
    public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Report type is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.FreelancerId)
                .NotEmpty().WithMessage("Freelancer ID is required.");
        }
    }
}
