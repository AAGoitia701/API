using API.Models.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data.Repository.IRepository;

namespace WebAPI.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public string CreateToken(AppUser appuser)
        {
            //Create claim

            var claim = new List<Claim>
            {
                //standard for JWT
                new Claim(JwtRegisteredClaimNames.Email, appuser.Email), 
                new Claim(JwtRegisteredClaimNames.GivenName, appuser.UserName)
            };

            //signing credentials -- type of encryption
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //create token as an object
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim), //-> wrapper for the claim
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            //creates the actual token
            var tokenHandler= new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
