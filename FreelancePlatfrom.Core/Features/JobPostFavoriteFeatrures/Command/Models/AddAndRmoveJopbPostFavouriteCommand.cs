using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Command.Models
{
    public class AddAndRmoveJopbPostFavouriteCommand : IRequest<ApiResponse<string>>
    {
        public int JobPostId { get; set; }
        public AddAndRmoveJopbPostFavouriteCommand(int jobPostId)
        {
            JobPostId = jobPostId;
        }
    }
}
