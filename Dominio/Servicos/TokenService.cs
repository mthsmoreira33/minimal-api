using Microsoft.IdentityModel.Tokens;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace minimal_api.Dominio.Servicos
{
    public class TokenService(string key) : ITokenService
    {
        private readonly string _key = key;

        public string GenerateJwtToken(Administrador administrador)
        {
            if (string.IsNullOrEmpty(_key)) return string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Role, administrador.Perfil),
                new("Email", administrador.Email),
                new("Perfil", administrador.Perfil)
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
