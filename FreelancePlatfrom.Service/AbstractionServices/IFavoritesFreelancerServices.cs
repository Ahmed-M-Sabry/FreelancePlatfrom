using FreelancePlatfrom.Data.Entities.FavoritesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IFavoritesFreelancerServices
    {
        Task<bool> IsFreelancerFavorited(string clientId, string freelancerId);
        Task<bool> AddFavoriteToFreelancer(FavoritesFreelancer favoritesFreelancer);
        Task RemoveFavoriteFromFreelancer(FavoritesFreelancer favoritesFreelancer);
        Task<List<FavoritesFreelancer>> GetAllFreelancersFavoritedByClient(string clientId);
        Task<List<string>> GetAllClientsWhoFavoritedFreelancer(string freelancerId);
        Task<FavoritesFreelancer> GetFavoritesFreelancerById(string freelancerId);
    }
}
