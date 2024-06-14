using ColorPaletteGeneratorApi.Models;
using ColorPaletteGeneratorApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public class AuthenticationTokenService(IConfiguration configuration) : IAuthenticationTokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly SecurityKey _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));

        /// <summary>
        /// Generates an authentication token for the specified AppUser.
        /// </summary>
        /// <param name="appUser"></param>
        public string GenerateAuthenticationToken(AppUser appUser)
        {
            List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new(ClaimTypes.Email, appUser.Email),
            ];

            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Subject = new(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new(_securityKey, SecurityAlgorithms.HmacSha512Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
