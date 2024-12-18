﻿namespace DoestaWeb.Models
{
    public class ClientsDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PassportNumber { get; set; }
        public int RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
