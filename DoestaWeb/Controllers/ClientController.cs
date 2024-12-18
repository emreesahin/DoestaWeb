using Microsoft.AspNetCore.Mvc;

namespace DoestaWeb.Controllers
{
    public class ClientController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
