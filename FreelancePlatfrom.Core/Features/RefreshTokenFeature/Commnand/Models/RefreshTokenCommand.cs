using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Data.Entities.Identity.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.RefreshTokenFeature.Commnand.Models
{
    public class RefreshTokenCommand : IRequest<ApiResponse<ResponseAuthModel>>
    {

    }
}
