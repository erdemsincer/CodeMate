using CodeMate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace CodeMate.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://authservice:8080/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Login failed.");
                return View(model);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var token = JsonDocument.Parse(responseJson).RootElement.GetProperty("token").GetString();

            HttpContext.Session.SetString("access_token", token!);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Register() => View();

        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var client = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://authservice:8080/api/auth/register", content);

            Console.WriteLine($"[WEB] Register response: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Kayıt başarısız.");
                return View(model);
            }

            return RedirectToAction("Login");
        }


    }
}
