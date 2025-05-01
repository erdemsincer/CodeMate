using MatchingService.Dtos;
using MatchingService.Entities;

namespace MatchingService.Services
{
    public interface IMatchingService
    {
        Task<List<MatchingDto>> GetAllMatchingsAsync();
        Task CreateMatchingAsync(Guid mentorId, Guid menteeId);
        Task<List<MatchingDto>> GetUserMatchingsAsync(Guid userId);
        Task<List<MatchingDto>> RecommendMatchingsAsync(Guid currentUserId);
    }
}
