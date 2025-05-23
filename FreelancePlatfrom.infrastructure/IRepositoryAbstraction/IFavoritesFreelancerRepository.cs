using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IFavoritesFreelancerRepository : IGenericRepositoryAsync<FavoritesFreelancer>
    {
        //Task<bool> AddFavoriteToFreelancer(string clientId, string freelancerId);
        //Task<bool> RemoveFavoriteFromFreelancer(string clientId, string freelancerId);
        Task<bool> IsFreelancerFavorited(string clientId, string freelancerId);

        Task<FavoritesFreelancer> GetFavoritesFreelancerById(string freelancerId);

        //Task<List<string>> GetAllFreelancersFavoritedByClient(string clientId);
        //Task<List<string>> GetAllClientsWhoFavoritedFreelancer(string freelancerId);
    }
}
