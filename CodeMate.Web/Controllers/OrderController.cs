using CodeMate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeMate.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var token = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://orderservice:8080/api/orders/my");
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return View(new List<OrderViewModel>());

            var orders = JsonSerializer.Deserialize<List<OrderViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(orders);
        }
    }
}
