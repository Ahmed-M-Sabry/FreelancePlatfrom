using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.JopPostFeatrures.Command.Models
{
    public class DeletejobPostCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public DeletejobPostCommand(int id)
        {
            Id = id;
        }
    }
}
