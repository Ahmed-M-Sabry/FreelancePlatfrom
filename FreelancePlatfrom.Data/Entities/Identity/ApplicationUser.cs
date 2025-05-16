using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.Identity.Helper;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Entities.Rating;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Data.Entities.Report;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.Identity
{
    /// <summary>
    /// Represents a user in the system (client or freelancer).
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        private string _firstName;
        private string _lastName;

        public ApplicationUser()
        {
            UserLanguages = new List<ApplicationUserLanguage>();
            UserSkills = new List<UserSkill>();
            JobPosts = new List<JobPost>();
            Portfolios = new List<Portfolio>();
            Reviews = new List<Review>();
            Reports = new List<Reports>();
            Contracts = new List<Contracts>();
            ApplyTasksAsFreelancer = new List<ApplyTask>();
            Favorites = new List<FavoritesFreelancer>();
            FavoritesJobPosts = new List<FavJobPost>();
            refreshTokens = new List<RefreshToken>();
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value ?? throw new ArgumentNullException(nameof(FirstName));
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value ?? throw new ArgumentNullException(nameof(LastName));
        }

        public string? ProfilePicture { get; set; }
        public string? YourTitle { get; set; }
        public string? Description { get; set; }
        public string? Education { get; set; }
        public string? Experience { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? Age { get; set; }
        public int? Zip { get; set; }
        public string? PortfolioUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastActive { get; set; }
        public string? ActiveOrNot { get; set; }
        public bool IsDeleted { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }

        public string? CountryId { get; set; }  // مفتاح البلد
        public Country country { get; set; }   // العلاقة

        public List<ApplicationUserLanguage> UserLanguages { get; set; }
        public List<UserSkill> UserSkills { get; set; }
        public List<JobPost> JobPosts { get; set; }
        public List<Portfolio> Portfolios { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Reports> Reports { get; set; }
        public List<Contracts> Contracts { get; set; }
        public ICollection<ApplyTask> ApplyTasksAsFreelancer { get; set; }
        public List<FavoritesFreelancer> Favorites { get; set; }
        public List<FavJobPost> FavoritesJobPosts { get; set; }
        public List<RefreshToken>? refreshTokens { get; set; }


    }
}
