using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Data.Entities.Identity.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.SignInFeatures.Command.Model
{
    public class SiginInUserCommand : IRequest<ApiResponse<ResponseAuthModel>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMeForMonth { get; set; } = false;
    }
}
