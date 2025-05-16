using FreelancePlatfrom.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.FavoritesTables
{
    /// <summary>
    /// Represents a freelancer marked as favorite by a client.
    /// </summary>
    public class FavoritesFreelancer
    {
        private int _id;
        private string _clientId;
        private string _freelancerId;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string ClientId
        {
            get => _clientId;
            set => _clientId = value ?? throw new ArgumentNullException(nameof(ClientId));
        }

        public ApplicationUser Client { get; set; }

        public string FreelancerId
        {
            get => _freelancerId;
            set => _freelancerId = value ?? throw new ArgumentNullException(nameof(FreelancerId));
        }

        public ApplicationUser Freelancer { get; set; }
    }
}
