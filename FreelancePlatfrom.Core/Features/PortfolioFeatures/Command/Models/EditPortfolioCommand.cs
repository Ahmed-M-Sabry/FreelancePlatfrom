using FreelancePlatfrom.Core.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models
{
    public class EditPortfolioCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string? Url { get; set; }
        public IFormFile? Media { get; set; }
    }
}
