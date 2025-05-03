using CodeMate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CodeMate.Web.Controllers
{
    public class OfferController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OfferController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Create(Guid courseId)
        {
            var model = new OfferCreateViewModel { CourseId = courseId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferCreateViewModel model)
        {
            var token = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model);
            Console.WriteLine($"[WEB] JSON: {json}");

            var response = await client.PostAsync("http://offerservice:8080/api/offers", new StringContent(json, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[WEB] Offer response: {response.StatusCode} | Body: {responseBody}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Teklif başarısız: " + responseBody);
                return View(model);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> MyOffers()
        {
            var token = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://offerservice:8080/api/offers/my");
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"[WEB] MyOffers StatusCode: {response.StatusCode}");
            Console.WriteLine($"[WEB] MyOffers JSON Body: {json}");

            var offers = JsonSerializer.Deserialize<List<OfferViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(offers);
        }



    }
}
