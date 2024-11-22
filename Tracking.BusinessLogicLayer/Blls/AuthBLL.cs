using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tracking.BusinessLogicLayer.DTOs;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.BusinessLogicLayer.Blls
{
    public interface IAuthBLL
    {
        Task<TokenDTO> Login(Guid id);
    }
    public class AuthBLL : IAuthBLL
    {
        private readonly IUserBLL _userBLL;
        private readonly IConfiguration _configuration;
        public AuthBLL(IUserBLL userBLL, IConfiguration configuration)
        {
            _userBLL = userBLL;
            _configuration = configuration;
        }

        public async Task<TokenDTO> Login(Guid id)
        {
            var response = await _userBLL.Get(id);
            if (response == null)
                throw new Exception("User not found");
            return BuildToken(new User { Id = response.Id});

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
