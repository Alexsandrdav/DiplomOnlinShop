using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace DiplomOnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {

        private AppSetting options;
        private readonly OnlineShopContext dbContext;

        public AdminController(IOptions<AppSetting> options, OnlineShopContext dbContext) {

            this.dbContext = dbContext;
            this.options = options.Value;
        }


        [HttpPost]
        [Route("login")]
        public ActionResult Create(AdminModel adminModel)
        {
            var admin = dbContext.Admins.FirstOrDefault(x=>x.Email == adminModel.Email);
            if (admin == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            var hash = HashPassword(adminModel.Password);
            if (admin.PasswordHash == hash)
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
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();

            var asBytes = Encoding.UTF8.GetBytes(password);

            var hashed = sha.ComputeHash(asBytes);

            return Convert.ToBase64String(hashed);
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