using jukebox.backend.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jukebox.backend.Repositories
{
    public class TokenService
    {
        public static string GenerateToken(Usuario user)
        {
            var manipuladorTokens = new JwtSecurityTokenHandler();

            var chave = Encoding.ASCII.GetBytes(JukeBoxSettings.Secret);

            var descritorToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Funcao.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manipuladorTokens.CreateToken(descritorToken);

            return manipuladorTokens.WriteToken(token);
        }
    }
}
