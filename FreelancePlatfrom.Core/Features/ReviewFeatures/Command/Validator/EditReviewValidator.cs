using FluentValidation;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Validator
{
    public class EditReviewValidator : AbstractValidator<EditReviewCommand>
    {
        public EditReviewValidator()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 5)
                .WithMessage("Rate must be between 1 and 5.");

            RuleFor(x => x.TaskCompletesPercentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Percentage must be between 0 and 100.");
        }
    }
}
