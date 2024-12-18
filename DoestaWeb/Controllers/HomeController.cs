using DoestaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoestaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
    //    private readonly IHttpClientFactory _httpClientFactory;

    //    public HomeController(IHttpClientFactory httpClientFactory)
    //    {
    //        _httpClientFactory = httpClientFactory;
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Index()
    //    {
    //        var client = _httpClientFactory.CreateClient();
    //        var responseMessage = await client.GetAsync("https://localhost:7010/api/Auth/register");
    //        if (responseMessage.IsSuccessStatusCode)
    //        {
    //            return RedirectToAction("Index", "Login");
    //        }
    //    }

    public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
