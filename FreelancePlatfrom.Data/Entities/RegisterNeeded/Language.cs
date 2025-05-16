using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Data.Entities.RegisterNeeded
{
    /// <summary>
    /// Represents a language entity.
    /// </summary>
    public class Language
    {
        private string _id;
        private string _value;

        public string Id
        {
            get => _id;
            set => _id = value ?? throw new ArgumentNullException(nameof(Id));
        }

        public string Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(Value));
        }

        public bool IsDeleted { get; set; }

        public List<ApplicationUserLanguage> UserLanguages { get; set; }
    }
}
