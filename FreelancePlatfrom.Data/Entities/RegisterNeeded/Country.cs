using FreelancePlatfrom.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.RegisterNeeded
{
    /// <summary>
    /// Represents a country entity.
    /// </summary>
    public class Country
    {
        private string _id;
        private string _iso;
        private string _name;
        private string _niceName;
        private string _iso3;
        private int _phoneCode;

        public Country()
        {
            Users = new List<ApplicationUser>();
            IsDeleted = false;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public string Iso
        {
            get => _iso;
            set => _iso = value ?? throw new ArgumentNullException(nameof(Iso));
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public string NiceName
        {
            get => _niceName;
            set => _niceName = value ?? throw new ArgumentNullException(nameof(NiceName));
        }

        public string Iso3
        {
            get => _iso3;
            set => _iso3 = value ?? throw new ArgumentNullException(nameof(Iso3));
        }

        public int? NumCode { get; set; }

        public int PhoneCode
        {
            get => _phoneCode;
            set => _phoneCode = value >= 0 ? value : throw new ArgumentException("PhoneCode cannot be negative.");
        }

        public bool IsDeleted { get; set; }

        
        public List<ApplicationUser> Users { get; set; }
    }
}
