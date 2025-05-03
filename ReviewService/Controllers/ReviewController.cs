using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Dtos;
using ReviewService.Services;
using System.Security.Claims;

namespace ReviewService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // ✅ Yorum oluştur
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCreateDto dto)
        {
            var reviewerIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(reviewerIdStr, out var reviewerId))
                return Unauthorized();

            await _reviewService.CreateReviewAsync(reviewerId, dto);
            return Ok(new { message = "Review created successfully." });
        }

        // ✅ Kullanıcıya yapılan yorumları getir
        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewsByUserId(Guid userId)
        {
            var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
            return Ok(reviews);
        }
    }
}
