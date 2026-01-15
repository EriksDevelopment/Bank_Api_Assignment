using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Core.Services.JwtServices
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int customerId, string role)
        {
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT key missing");

            var claims = new[]
            {
            new Claim("CustomerId", customerId.ToString()),
            new Claim(ClaimTypes.Role, role)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(
                    _configuration.GetValue<int>("Jwt:TokenValidityMins")
                ),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(handler.CreateToken(tokenDescriptor));
        }
    }
}