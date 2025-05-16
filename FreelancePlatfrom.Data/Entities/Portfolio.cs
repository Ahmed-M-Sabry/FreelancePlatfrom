using FreelancePlatfrom.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities
{
    /// <summary>
    /// Represents a portfolio entry for a freelancer.
    /// </summary>
    public class Portfolio
    {
        private int _id;
        private string _name;
        private string _description;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(Description));
        }

        public string? Media { get; set; }
        public string? Url { get; set; }
        public DateTime? ProjectDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
