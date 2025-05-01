using MatchingService.Data;
using MatchingService.Dtos;
using MatchingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchingService.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly MatchingDbContext _context;

        public MatchingService(MatchingDbContext context)
        {
            _context = context;
        }

        public async Task<List<MatchingDto>> GetAllMatchingsAsync()
        {
            return await _context.Matchings
                .Select(m => new MatchingDto
                {
                    MentorId = m.MentorId,
                    MenteeId = m.MenteeId
                })
                .ToListAsync();
        }

        public async Task CreateMatchingAsync(Guid mentorId, Guid menteeId)
        {
            var matching = new Matching
            {
                MentorId = mentorId,
                MenteeId = menteeId,
                MatchedAt = DateTime.UtcNow
            };

            _context.Matchings.Add(matching);
            await _context.SaveChangesAsync();
        }
    }
}
