using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace FreelancePlatfrom.Data.Entities.Identity.Helper
{
    public class ResponseAuthModel
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        [JsonIgnore]
        public CookieOptions CookieOptions { get; set; } // New property for cookie settings
    }
}