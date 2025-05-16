using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.Register.LogoutFeature.Command.Model
{
    public class LogoutCommand : IRequest<ApiResponse<string>>
    {
        
    }

}
