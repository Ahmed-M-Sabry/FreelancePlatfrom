using FluentValidation;
using FreelancePlatfrom.Core.Features.ClientRegister.Commands.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ClientRegister.Commands.Validators
{
    /// <summary>
    /// Validator for the AddClientCommand to ensure valid client registration data.
    /// </summary>
    public class AddClientValidator : AbstractValidator<AddClientCommand>
    {
        public AddClientValidator()
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

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("Country is required.");
        }
    }
}

