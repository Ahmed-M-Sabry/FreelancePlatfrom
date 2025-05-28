using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Results
{
    public class GetMyFavoritesFreelancerResponse
    {
        public string FreelancerId { get; set; }
        public string FreelancerName { get; set; }
        public string FreelancerImage { get; set; }
    }
}
