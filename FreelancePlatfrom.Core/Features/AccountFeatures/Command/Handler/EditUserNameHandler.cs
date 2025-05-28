using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Commands.Handlers
{
    public class EditUserNameHandler : ResponseHandler,
        IRequestHandler<EditUserNameCommand, ApiResponse<string>>
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditUserNameHandler(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<string>> Handle(EditUserNameCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<string>();

            var user = await _accountService.GetApplicationUserByIdAsync(userId);
            if (user is null)
                return NotFound<string>("User not found.");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            await _accountService.UpdateUserAsync(user);

            return Created("User name updated successfully.");
        }
    }
}
