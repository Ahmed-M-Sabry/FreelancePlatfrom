using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model
{
    public class GenerateResetTokenCommand : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; }
    }
}
