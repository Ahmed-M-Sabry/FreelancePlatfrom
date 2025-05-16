using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Identity.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IAuthenticatioService
    {
        Task<string> CreateJwtToken(ApplicationUser user);

        Task<ResponseAuthModel> RefreshTokenAsunc(string token);

        Task<ResponseAuthModel> GenerateAuthModelAsync(ApplicationUser user, bool rememberMe);
        Task<bool> RevokeRefreshTokenFromCookiesAsync();

    }
}
