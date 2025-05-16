using FreelancePlatfrom.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.Report
{
    /// <summary>
    /// Represents a report submitted by a client about a freelancer.
    /// </summary>
    public class Reports
    {
        private int _id;
        private string _type;
        private string _description;
        private DateTime _reportDate;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value ?? throw new ArgumentNullException(nameof(Type));
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(Description));
        }

        public DateTime ReportDate
        {
            get => _reportDate;
            set => _reportDate = value;
        }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public string FreelancerId { get; set; }
        public ApplicationUser Freelancer { get; set; }

        public bool IsDeleted { get; set; }
    }
}
