using DoestaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoestaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Clients clients = new Clients();
        private IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<Clients> Register(ClientsDto request)
        {
            string Surname = BCrypt.Net.BCrypt.HashPassword(request.Surname); // Encryption örneği

            clients.Name = request.Name;
            clients.Surname = Surname;

            string token = CreateToken(clients);
            return Ok(token);
        }
        
        
        [HttpPost("login")]
        public ActionResult<Clients> Login(ClientsDto request)
        {
            if (clients.Name != request.Name) 
            {
                return BadRequest("User not found.");          
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Surname, clients.Surname))
            {
                return BadRequest("Wrong password");
            }
            string token = CreateToken(clients);
            return Ok(token);
        }

        private string CreateToken(Clients clients)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, clients.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
