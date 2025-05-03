using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public OrdersController(OrderDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var guid = Guid.Parse(userId);
            var orders = _context.Orders.Where(o => o.UserId == guid).ToList();

            var client = _httpClientFactory.CreateClient();
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = new List<OrderWithCourseDto>();

            foreach (var order in orders)
            {
                string courseTitle = "Bilinmiyor";

                var response = await client.GetAsync($"http://courseservice:8080/api/courses/{order.CourseId}");
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var course = JsonSerializer.Deserialize<CourseDto>(body, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    courseTitle = course?.Title ?? "Bilinmiyor";
                }

                result.Add(new OrderWithCourseDto
                {
                    Id = order.Id,
                    CourseId = order.CourseId,
                    CourseTitle = courseTitle,
                    CreatedAt = order.CreatedAt,
                    IsPaid = order.IsPaid
                });
            }

            return Ok(result);
        }
    }
}
