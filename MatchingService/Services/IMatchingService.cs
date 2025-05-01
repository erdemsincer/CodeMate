using MatchingService.Dtos;
using MatchingService.Entities;

namespace MatchingService.Services
{
    public interface IMatchingService
    {
        Task<List<MatchingDto>> GetAllMatchingsAsync();
        Task CreateMatchingAsync(Guid mentorId, Guid menteeId);
        
    }
}
