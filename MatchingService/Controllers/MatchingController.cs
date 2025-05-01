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

        // 🔹 Tüm eşleşmeleri getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matchings = await _matchingService.GetAllMatchingsAsync();
            return Ok(matchings);
        }

        // 🔹 Yeni eşleşme oluştur
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchingDto dto)
        {
            await _matchingService.CreateMatchingAsync(dto.MentorId, dto.MenteeId);
            return Ok(new { message = "Matching created." });
        }

        // 🔹 Belirli kullanıcıya ait eşleşmeleri getir
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserMatchings(Guid userId)
        {
            var matchings = await _matchingService.GetUserMatchingsAsync(userId);
            return Ok(matchings);
        }

        // 🔹 Önerilen eşleşmeleri getir
        [HttpGet("recommend/{userId}")]
        public async Task<IActionResult> Recommend(Guid userId)
        {
            var recommended = await _matchingService.RecommendMatchingsAsync(userId);
            return Ok(recommended);
        }
    }
}
