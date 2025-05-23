using FreelancePlatfrom.Data.Entities.FavoritesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IFavoriteJobPostService
    {
        Task<FavJobPost> IsJobPostFavorited(string freelancerId, int jobPostId);
        Task AddJobPostFavourite(FavJobPost favJobPost);
        Task RemoveJobPostFavourite(FavJobPost favJobPost);
    }
}
