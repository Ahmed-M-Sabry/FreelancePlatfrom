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
        public int Id { get; set; }

        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }

        public string FreelancerId { get; set; }

        public ApplicationUser Freelancer { get; set; }
    }

}
