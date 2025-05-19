﻿using Azure;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Identity.Helper;
using FreelancePlatfrom.Service.AbstractionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.ImplementationServices
{
    public class AuthenticatioService : IAuthenticatioService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSittings _jwt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatioService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            JwtSittings jwt, IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwt = jwt;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwt.DurationInHours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ResponseAuthModel> RefreshTokenAsunc(string token)
        {
            var responseAuthModel = new ResponseAuthModel();
            var user = await _userManager.Users
                .Include(u => u.refreshTokens)
                .SingleOrDefaultAsync(u => u.refreshTokens.Any(t => t.token == token));

            if (user == null)
            {
                responseAuthModel.Message = "User Not Found";
                return responseAuthModel;
            }

            var refreshToken = user.refreshTokens.Single(t => t.token == token);

            if (!refreshToken.IsActive)
            {
                responseAuthModel.Message = "Inactive Token";
                return responseAuthModel;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            user.refreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
            var jwtToken = await CreateJwtToken(user);

            responseAuthModel.Token = jwtToken;
            var roles = await _userManager.GetRolesAsync(user);
            responseAuthModel.Roles = roles.ToList();
            responseAuthModel.RefreshToken = newRefreshToken.token;
            responseAuthModel.RefreshTokenExpiration = newRefreshToken.ExpireOn;
            responseAuthModel.CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = newRefreshToken.ExpireOn,
                Path = "/"
            };

            return responseAuthModel;
        }

        public async Task<bool> RevokeRefreshTokenFromCookiesAsync()
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return false;

            var user = await _userManager.Users
                .Include(u => u.refreshTokens)
                .FirstOrDefaultAsync(u => u.refreshTokens.Any(t => t.token == refreshToken));

            if (user == null)
                return false;

            var token = user.refreshTokens.SingleOrDefault(t => t.token == refreshToken);

            if (token == null || !token.IsActive)
                return false;

            token.RevokedOn = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return false;

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("RefreshToken", new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return true;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                token = Convert.ToBase64String(randomNumber),
                CreatedOn = DateTime.UtcNow,
                ExpireOn = DateTime.UtcNow.AddDays(7)
            };
        }

        public async Task<ResponseAuthModel> GenerateAuthModelAsync(ApplicationUser user, bool rememberMe)
        {
            var jwtToken = await CreateJwtToken(user);

            RefreshToken refreshToken;

            var existingActiveToken = user.refreshTokens.FirstOrDefault(r => r.IsActive);

            if (existingActiveToken != null)
            {
                refreshToken = existingActiveToken;
            }
            else
            {
                refreshToken = GenerateRefreshToken();

                if (rememberMe)
                    refreshToken.ExpireOn = DateTime.UtcNow.AddDays(30);

                user.refreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new ResponseAuthModel
            {
                Message = "Login successful.",
                Token = jwtToken,
                Roles = roles.ToList(),
                RefreshToken = refreshToken.token,
                RefreshTokenExpiration = refreshToken.ExpireOn,
                CookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = refreshToken.ExpireOn,
                    Path = "/"
                }
            };
        }
    }
}