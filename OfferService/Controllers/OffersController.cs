using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfferService.Data;
using OfferService.Dtos;
using OfferService.Entities;
using System.Security.Claims;

namespace OfferService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OffersController : ControllerBase
    {
        private readonly OfferDbContext _context;

        public OffersController(OfferDbContext context)
        {
            _context = context;
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
        public IActionResult GetMyOffers()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var guid = Guid.Parse(userId);
            var myOffers = _context.Offers.Where(o => o.UserId == guid).ToList();
            return Ok(myOffers);
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
            if (offer is null) return NotFound();

            offer.IsApproved = true;
            await _context.SaveChangesAsync();

            return Ok(offer);
        }
    }
}
