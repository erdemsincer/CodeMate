using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfferService.Data;
using OfferService.Dtos;
using OfferService.Entities;
using OrderService.Events;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace OfferService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OffersController : ControllerBase
    {
        private readonly OfferDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;


        public OffersController(OfferDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] OfferCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var offer = new Offer
            {
                UserId = Guid.Parse(userId),
                CourseId = dto.CourseId,
                OfferedPrice = dto.OfferedPrice
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return Ok(offer);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyOffers([FromServices] IHttpClientFactory httpClientFactory)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var offers = _context.Offers.Where(o => o.UserId == userId).ToList();

            var httpClient = httpClientFactory.CreateClient();

            // 🛡️ Token'ı al ve header'a ekle
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = new List<OfferWithCourseDto>();

            foreach (var offer in offers)
            {
                string courseTitle = "Bilinmiyor";

                var response = await httpClient.GetAsync($"http://courseservice:8080/api/courses/{offer.CourseId}");
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var course = JsonSerializer.Deserialize<CourseDto>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    courseTitle = course?.Title ?? "Bilinmiyor";
                }

                result.Add(new OfferWithCourseDto
                {
                    Id = offer.Id,
                    CourseId = offer.CourseId,
                    CourseTitle = courseTitle,
                    OfferedPrice = offer.OfferedPrice,
                    IsApproved = offer.IsApproved,
                    CreatedAt = offer.CreatedAt
                });
            }

            return Ok(result);
        }



        [HttpGet("course/{courseId}")]
        public IActionResult GetOffersForCourse(Guid courseId)
        {
            var offers = _context.Offers.Where(o => o.CourseId == courseId).ToList();
            return Ok(offers);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveOffer(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer == null) return NotFound();

            offer.IsApproved = true;
            await _context.SaveChangesAsync();

            // ✅ OrderService'e event gönder
            var eventMessage = new OfferApprovedEvent
            {
                OfferId = offer.Id,
                UserId = offer.UserId,
                CourseId = offer.CourseId,
                OfferedPrice = offer.OfferedPrice
            };

            await _publishEndpoint.Publish(eventMessage);

            return Ok(offer);
        }
    }
}
