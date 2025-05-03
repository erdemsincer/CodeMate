using Microsoft.EntityFrameworkCore;
using ReviewService.Data;
using ReviewService.Dtos;
using ReviewService.Entities;

namespace ReviewService.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewDbContext _context;

        public ReviewService(ReviewDbContext context)
        {
            _context = context;
        }

        public async Task CreateReviewAsync(Guid reviewerId, ReviewCreateDto dto)
        {
            var review = new Review
            {
                ReviewerId = reviewerId,
                RevieweeId = dto.RevieweeId,
                Content = dto.Content,
                Rating = dto.Rating,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReviewDto>> GetReviewsByUserIdAsync(Guid userId)
        {
            return await _context.Reviews
                .Where(r => r.RevieweeId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReviewDto
                {
                    ReviewerId = r.ReviewerId,
                    RevieweeId = r.RevieweeId,
                    Content = r.Content,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }
    }
}
