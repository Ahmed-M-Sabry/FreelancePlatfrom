using FluentValidation;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;

namespace FreelancePlatform.Core.Features.AuthenticationFeatures.ChangePasswordFeature.Validators
{
    /// <summary>
    /// Validator for the ChangePasswordCommand to ensure valid password change data.
    /// </summary>
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters.")
                .NotEqual(x => x.OldPassword).WithMessage("New password cannot be the same as the old password.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.NewPassword).WithMessage("Confirm password does not match new password.");
        }
    }
}