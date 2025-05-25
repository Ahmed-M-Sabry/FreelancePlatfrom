using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results
{
    public class GetAllLanguagesForUserResponse
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

    }
}
