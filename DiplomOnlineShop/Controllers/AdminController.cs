using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DiplomOnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {

        private const string Email = "ABSD@tut.by";
        private const string Password = "12345ABSD";
        private AppSetting options;

        public AdminController(IOptions<AppSetting> options) {
            this.options = options.Value;
        }


        [HttpPost]
        [Route("login")]
        public ActionResult Create(AdminModel adminModel)
        {
            if (Email == adminModel.Email && Password == adminModel.Password)
            {
                SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(adminModel.Email);
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(securityToken);

                return StatusCode(StatusCodes.Status200OK, new TokenModel { Token = token });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

        }

        private SecurityTokenDescriptor GetTokenDescriptor(string email)
        {
            const int expiringDays = 7;
            var dict = new Dictionary<string, object>();
            dict.Add("email", email);

            byte[] securityKey = Encoding.UTF8.GetBytes(options.EncryptionKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(securityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(expiringDays),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                Claims = dict
            };

            return tokenDescriptor;
        }
    }    
}