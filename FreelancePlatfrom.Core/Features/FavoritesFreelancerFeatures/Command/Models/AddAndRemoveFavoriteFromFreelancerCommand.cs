using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Command.Models
{
    public class AddAndRemoveFavoriteFromFreelancerCommand : IRequest<ApiResponse<string>>
    {
        public string UserId { get; set; }
        public AddAndRemoveFavoriteFromFreelancerCommand(string userId)
        {
            UserId = userId;
        }
    }
}
