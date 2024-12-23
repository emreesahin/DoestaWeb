using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using DoestaWeb.Models;

namespace DoestaWeb.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            // Yeni bir ClientsViewModel nesnesi oluştur ve View'e gönder
            var model = new ClientsViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(ClientsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
               
               

                var client = _httpClientFactory.CreateClient();

                // API'ye gönderilecek JSON içeriği
                var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                // API'ye POST isteği gönderme
                var response = await client.PostAsync("https://localhost:7010/api/ClientsApi/Register", jsonContent);
               
                if (response.IsSuccessStatusCode)
                {
                    // Başarılı işlem sonrası başka bir sayfaya yönlendir
                    return RedirectToAction("Index", "Home");
                }
                else
                    {
                        // Daha ayrıntılı hata mesajı ekleyin
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"API hatası: {errorMessage}");
                    }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Beklenmeyen bir hata oluştu: {ex.Message}");
            }

            return View(model);
        }


    }
}
