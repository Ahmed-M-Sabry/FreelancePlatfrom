using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatform.Core.Features.AuthenticationFeatures.ChangePasswordFeature.Command.Handler
{
    public class ChangePasswordCommandHandler : ResponseHandler, IRequestHandler<ChangePasswordCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<ChangePasswordCommand> _validator;

        public ChangePasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IValidator<ChangePasswordCommand> validator)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
        }

        public async Task<ApiResponse<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ValidationFailed<string>(errors);
            }

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!isOldPasswordValid)
                return BadRequest<string>("Old password is incorrect.");

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ValidationFailed<string>(errors);
            }

            return Success<string>("Password changed successfully.");
        }
    }
}