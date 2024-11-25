using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Models.DTOs.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Tracking.DataAccessLayer.Entities;

namespace Tracking.BusinessLogicLayer.Blls
{
    public interface IAuthBLL
    {
        Task<TokenDTO> Login(Guid id);
        Task<UserViewModel> Logged();
    }
    public class AuthBLL : IAuthBLL
    {
        private readonly IUserBLL _userBLL;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthBLL(IUserBLL userBLL, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userBLL = userBLL;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<TokenDTO> Login(Guid id)
        {
            var response = await _userBLL.Get(id);
            if (response == null)
                throw new Exception("User not found");
            return BuildToken(new User { Id = response.Id });

        }
        public async Task<UserViewModel> Logged()
        {

            if (_httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                var isGuid = Guid.TryParse(_httpContextAccessor.HttpContext.User.Identity.Name, out var id);
                if (isGuid)
                {
                    return await _userBLL.Get(id);
                }
                //if (_httpContextAccessor.HttpContext.User.Identity.Name is Guid id) 
                //{

                //}
                //var user = Guid._httpContextAccessor.HttpContext.User.Identity.Name.ToString()

                // _httpContextAccessor.HttpContext.User.Identity.Name
                //    ?? "";
            }
            throw new NullReferenceException("Unauthenticated user");
        }
        private TokenDTO BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
