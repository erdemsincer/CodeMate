using ReviewService.Dtos;

namespace ReviewService.Services
{
    public interface IReviewService
    {
        Task CreateReviewAsync(Guid reviewerId, ReviewCreateDto dto);
        Task<List<ReviewDto>> GetReviewsByUserIdAsync(Guid userId);
    }
}
