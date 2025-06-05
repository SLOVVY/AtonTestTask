using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestTask_aton.Core.Models;
using TestTask_aton.Core.Abstractions;

namespace TestTask_aton.Infrastructure
{
    public class JWTProvider(IOptions<JwtOptions> options) : IJWTProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string GenerateToken(User user)
        {
            string role = "User";

            if (user.IsAdmin == true) role = "Admin";

            Claim[] claims = [
                new("userId", user.Id.ToString()),
                new("userLogin", user.Login),
                new("userName", user.Name),
                new("isAdmin", user.IsAdmin.ToString()),
                new(ClaimTypes.Role, role)
            ];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
