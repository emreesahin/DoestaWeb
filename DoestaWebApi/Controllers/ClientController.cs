using Microsoft.AspNetCore.Mvc;
using DoestaWeb.Data;
using DoestaWeb.Models;
using System.Linq;
using System.Text;

namespace DoestaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetClientInfo/{id}")]
        public IActionResult GetClientInfo(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
                return NotFound("Client not found");

            var response = new
            {
                Name = client.Name,
                Surname = client.Surname,
                CheckInDate = client.CheckInDate,
                CheckOutDate = client.CheckOutDate,
                RoomNumber = client.RoomNumber
            };

            return Ok(response);
        }

        [HttpPost("GenerateQRCode")]
        public IActionResult GenerateQRCode([FromBody] QRRequest request)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == request.Id && c.PassportNumber == request.PassportNumber);
            if (client == null)
                return Unauthorized("Invalid ID or Passport Number");

            var encryptedData = EncryptData($"{client.Name}|{client.Surname}|{client.RoomNumber}|{client.PassportNumber}");

            return Ok(new { EncryptedData = encryptedData });
        }

        private string EncryptData(string data)
        {
            var plainBytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(plainBytes);
        }
    }

    public class QRRequest
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; }
    }
}
