using MigrationTask.Models;
using MigrationTask.Services.TokenGenerators;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MigrationTask.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        
        public AccessTokenGenerator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(AdminLogin admin)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.AccessTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",admin.Id.ToString()),
                new Claim(ClaimTypes.Name,admin.username)
            };
            JwtSecurityToken token = new JwtSecurityToken(_configuration.Issuer,_configuration.Audience,claims,DateTime.UtcNow,DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes),credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}