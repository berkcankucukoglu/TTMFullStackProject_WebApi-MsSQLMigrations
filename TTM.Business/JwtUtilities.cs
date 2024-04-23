using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TTM.Domain;

namespace TTM.Business
{
    internal class JwtUtilities
    {
        internal SymmetricSecurityKey _key;
        internal JwtUtilities()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....tinytaskmanager.....securitytokentoken"));
        }

        internal string CreateJwt(User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role == 0 ? "User" : "Admin")
            });
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials,
            };
            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }

    }
}
