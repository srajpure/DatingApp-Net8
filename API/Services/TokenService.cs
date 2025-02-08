using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _configuration = null;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(AppUser appUser)
        {
            var tokenKey = _configuration["tokenkey"] ?? throw new Exception("cannot access Token Key from AppSettings");
            if (string.IsNullOrEmpty(tokenKey) && tokenKey.Length < 64)
            {throw new Exception("Token is empty or needs to be longer");}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claim = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier ,appUser.UserName)
            };

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}




