using FreelancePlatfrom.Data.Entities;
using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Entities.Rating;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Data.Entities.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Results
{
    public class FreelancerProfileResponse
    {
            public string Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string? ProfilePicture { get; set; }
            public string? YourTitle { get; set; }
            public string? Description { get; set; }
            public string? Education { get; set; }
            public string? Experience { get; set; }
            public decimal? HourlyRate { get; set; }
            public int? Age { get; set; }
            public int? Zip { get; set; }
            public string? PortfolioUrl { get; set; }
            public string? State { get; set; }
            public string? Address { get; set; }
            public string CountryName { get; set; }
            public List<string> LanguageName { get; set; }
            public List<string> SkillName { get; set; }
            public double AverageRating { get; set; }
        
    }
}
