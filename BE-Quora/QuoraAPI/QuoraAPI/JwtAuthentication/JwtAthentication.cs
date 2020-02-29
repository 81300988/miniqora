using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace QuoraAPI.JwtAuthentication
{
    public class JwtAthentication
    {
        public static string GenerateJSONWebToken(string key, string issuer, string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim("email", Email),
                new Claim("userId", UserId.ToString()),
            };

            var token = new JwtSecurityToken(issuer,
                                             issuer,
                                             claims: claims,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static int GetCurrentUserId(string tokenString)
        {
            var jwtEncodedString = tokenString.Substring(7); // trim 'Bearer ' from the start since its just a prefix for the token string
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);

            Int32.TryParse(token.Claims.First(c => c.Type == "userId").Value, out int userId);

            return userId;
        }
    }
}
