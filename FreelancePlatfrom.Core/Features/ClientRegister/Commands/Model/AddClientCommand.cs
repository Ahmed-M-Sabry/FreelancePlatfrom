using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ClientRegister.Commands.Model
{/// <summary>
 /// Represents a command to register a new client with essential fields.
 /// </summary>
    public class AddClientCommand : IRequest<ApiResponse<string>>
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _countryId;  // غيرت هنا

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

        public string Email
        {
            get => _email;
            set => _email = value ?? throw new ArgumentNullException(nameof(Email));
        }

        public string Password
        {
            get => _password;
            set => _password = value ?? throw new ArgumentNullException(nameof(Password));
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = value ?? throw new ArgumentNullException(nameof(ConfirmPassword));
        }

        public string CountryId
        {
            get => _countryId;
            set => _countryId = value ?? throw new ArgumentNullException(nameof(CountryId));
        }
    }
}
