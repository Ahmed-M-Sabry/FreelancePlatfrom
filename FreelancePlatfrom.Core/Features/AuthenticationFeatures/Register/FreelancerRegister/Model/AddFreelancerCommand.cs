﻿using FreelancePlatfrom.Core.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.Register.FreelancerRegister.Model
{

    /// <summary>
    /// Represents a command to register a new client.
    /// </summary>
    public class AddFreelancerCommand : IRequest<ApiResponse<string>>
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _phoneNumber;
        private string _countryId;
        private string _state;
        private string _address;

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

        public List<string>? SelectedLanguages { get; set; }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public int? Age { get; set; }

        public string? YourTitle { get; set; }

        public string? Description { get; set; }

        public string? Education { get; set; }

        public string? Experience { get; set; }

        public decimal? HourlyRate { get; set; }

        public List<int>? SelectedSkills { get; set; }

        public string CountryId
        {
            get => _countryId;
            set => _countryId = value ?? throw new ArgumentNullException(nameof(CountryId));
        }

        public string State
        {
            get => _state;
            set => _state = value ?? throw new ArgumentNullException(nameof(State));
        }

        public string Address
        {
            get => _address;
            set => _address = value ?? throw new ArgumentNullException(nameof(Address));
        }

        public int ZIP { get; set; }

        public string? PortfolioUrl { get; set; }

        public IFormFile? ProfilePicture { get; set; }
        public string? ProfilePicturePath { get; set; }

    }
}
