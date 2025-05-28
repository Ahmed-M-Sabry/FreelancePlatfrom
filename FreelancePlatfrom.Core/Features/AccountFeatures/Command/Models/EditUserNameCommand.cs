using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models
{
    public class EditUserNameCommand : IRequest<ApiResponse<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
