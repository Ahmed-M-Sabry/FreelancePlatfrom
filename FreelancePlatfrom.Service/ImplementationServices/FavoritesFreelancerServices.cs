using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class FavoritesFreelancerServices : IFavoritesFreelancerServices
    {
        private readonly IFavoritesFreelancerRepository _favoritesFreelancerRepository;
        public FavoritesFreelancerServices(IFavoritesFreelancerRepository favoritesFreelancerRepository)
        {
            _favoritesFreelancerRepository = favoritesFreelancerRepository;
        }

        public async Task<bool> AddFavoriteToFreelancer(FavoritesFreelancer favoritesFreelancer)
        {
            await _favoritesFreelancerRepository.AddAsync(favoritesFreelancer);
            return true;
        }
        public Task<List<string>> GetAllClientsWhoFavoritedFreelancer(string freelancerId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FavoritesFreelancer>> GetAllFreelancersFavoritedByClient(string clientId)
        {
            return await _favoritesFreelancerRepository.GetAllFreelancersFavoritedByClient(clientId);
        }

        public async Task<FavoritesFreelancer> GetFavoritesFreelancerById(string freelancerId)
        {
            return await _favoritesFreelancerRepository.GetFavoritesFreelancerById(freelancerId);
        }

        public async Task<bool> IsFreelancerFavorited(string clientId, string freelancerId)
        {
            return await _favoritesFreelancerRepository.IsFreelancerFavorited(clientId,freelancerId);
        }

        public Task RemoveFavoriteFromFreelancer(FavoritesFreelancer favoritesFreelancer)
        {
            return  _favoritesFreelancerRepository.DeleteAsync(favoritesFreelancer);
        }
    }
}
