using FluentValidation;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Validator
{
    public class CreateJobPostValidator : AbstractValidator<CreateJobPostCommand>
    {
        public CreateJobPostValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(10, 1000).WithMessage("Description must be between 10 and 1000 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Valid Category ID is required.");

            RuleFor(x => x.DurationTime)
                .Must(dt => !dt.HasValue || dt.Value > DateTime.UtcNow)
                .When(x => x.DurationTime.HasValue)
                .WithMessage("Duration time must be in the future.");

            RuleFor(x => x.SkillIds)
                .NotNull().WithMessage("Skill IDs are required.")
                .Must(skills => skills == null || skills.All(id => id > 0))
                .WithMessage("Skill IDs must be positive integers.");
        }
    }
}
