using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CountryFeatures.Command.Models
{
    public class RestorCountryCommand : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; }
        public RestorCountryCommand(string id)
        {
            Id = id;
        }
    }
}
