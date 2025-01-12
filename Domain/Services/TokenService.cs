using Microsoft.IdentityModel.Tokens;
using minimal_api.Domain.Interfaces;
using minimal_api.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace minimal_api.Domain.Services
{
    public class TokenService(string key) : ITokenService
    {
        private readonly string _key = key;

        public string GenerateJwtToken(Admin admin)
        {
            if (string.IsNullOrEmpty(_key)) return string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Email, admin.Email),
                new(ClaimTypes.Role, admin.Role),
                new("Email", admin.Email),
            };

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
