using FluentValidation;
using FreelancePlatfrom.Core.Features.Register.FreelancerRegister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.Register.FreelancerRegister.Validators
{
    /// <summary>
    /// Validator for the AddClientCommand to ensure valid client registration data.
    /// </summary>
    public class AddFreelacerValidator : AbstractValidator<AddFreelancerCommand>
    {
        public AddFreelacerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters.")
                .MaximumLength(25).WithMessage("First name cannot exceed 25 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters.")
                .MaximumLength(25).WithMessage("Last name cannot exceed 25 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.SelectedLanguages)
                .NotEmpty().WithMessage("At least one language is required.")
                .When(x => x.SelectedLanguages != null);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Age is required.")
                .InclusiveBetween(18, 100).WithMessage("Age must be between 18 and 100.");

            RuleFor(x => x.YourTitle)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Education)
                .MaximumLength(100).WithMessage("Education cannot exceed 100 characters.")
                .When(x => x.Education != null);

            RuleFor(x => x.Experience)
                .MaximumLength(1000).WithMessage("Experience cannot exceed 1000 characters.")
                .When(x => x.Experience != null);

            RuleFor(x => x.HourlyRate)
                .InclusiveBetween(0, 10000).WithMessage("Hourly rate must be between 0 and 10000.")
                .When(x => x.HourlyRate.HasValue);

            RuleFor(x => x.SelectedSkills)
                .NotEmpty().WithMessage("At least one skill is required.")
                .When(x => x.SelectedSkills != null);

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.");

            RuleFor(x => x.ZIP)
                .GreaterThan(0).WithMessage("ZIP code must be greater than 0.");

            RuleFor(x => x.PortfolioUrl)
                .Must(BeAValidUrl).WithMessage("Invalid portfolio URL.")
                .When(x => x.PortfolioUrl != null);
        }

        private bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return true;
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}