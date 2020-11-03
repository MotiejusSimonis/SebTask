using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SEBtask.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Client client)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, client.PersonalId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, client.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        public long GetClientPersonalId(ClaimsPrincipal user)
        {
            string userId = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return long.Parse(userId);
        }

        public bool HasClaims(ClaimsPrincipal user)
        {
            return user.Claims.Any();
        }
    }
}
