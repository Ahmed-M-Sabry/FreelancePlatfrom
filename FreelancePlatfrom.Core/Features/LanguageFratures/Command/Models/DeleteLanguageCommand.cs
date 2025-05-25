using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Command.Models
{
    public class DeleteLanguageCommand : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; }
    }
}
