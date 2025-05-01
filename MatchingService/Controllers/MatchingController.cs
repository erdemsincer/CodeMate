using MatchingService.Data;
using MatchingService.Dtos;
using MatchingService.Entities;
using MatchingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MatchingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MatchingController : ControllerBase
    {
        private readonly IMatchingService _matchingService;

        public MatchingController(IMatchingService matchingService)
        {
            _matchingService = matchingService;
        }

        // 🔥 Tüm eşleşmeleri listele
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matchings = await _matchingService.GetAllMatchingsAsync();
            return Ok(matchings);
        }

        // 🔥 Yeni eşleşme oluştur (mentee giriş yapan kullanıcı, mentor dışardan gelir)
        [HttpPost("{mentorId}")]
        public async Task<IActionResult> CreateMatching(Guid mentorId)
        {
            var menteeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (menteeId == null)
                return Unauthorized();

            await _matchingService.CreateMatchingAsync(mentorId, Guid.Parse(menteeId));
            return Ok(new { message = "Eşleşme başarıyla oluşturuldu." });
        }
    }
}
