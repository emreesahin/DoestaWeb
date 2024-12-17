using Microsoft.AspNetCore.Identity;

namespace DoestaWeb.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }

    }
}
