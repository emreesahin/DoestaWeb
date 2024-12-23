using Microsoft.AspNetCore.Mvc;
using DoestaWeb.Data;
using DoestaWeb.Models;

namespace DoestaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] Clients model)
        {
            if (model == null)
                return BadRequest("Invalid client data.");

            try
            {
               
                _context.Clients.Add(model);
                _context.SaveChanges();
                return Ok("Client registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
