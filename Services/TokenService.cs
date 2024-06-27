using JwtAspNet.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAspNet.Services
{
    public class TokenService
    {
        public string Create(Usuario usuario)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(usuario)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(Usuario usuario)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim("id", usuario.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, usuario.Email));
            claims.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
            claims.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nome));

            foreach (var role in usuario.Roles)
                claims.AddClaim(new Claim(ClaimTypes.Role, role));

            return claims;
        }
    }
}
