using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model
{
    public class ChangePasswordCommand : IRequest<ApiResponse<string>>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; } 
    }
}
