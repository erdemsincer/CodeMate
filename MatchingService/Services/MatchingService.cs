using System.Net;
using MatchingService.Data;
using MatchingService.Dtos;
using MatchingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchingService.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly MatchingDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public MatchingService(MatchingDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
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
                MenteeId = menteeId
            };

            _context.Matchings.Add(matching);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MatchingDto>> GetUserMatchingsAsync(Guid userId)
        {
            return await _context.Matchings
                .Where(m => m.MentorId == userId || m.MenteeId == userId)
                .Select(m => new MatchingDto
                {
                    MentorId = m.MentorId,
                    MenteeId = m.MenteeId
                })
                .ToListAsync();
        }

        public async Task<List<MatchingDto>> RecommendMatchingsAsync(Guid currentUserId)
        {
            var client = _httpClientFactory.CreateClient();

            var mySkillResponse = await client.GetAsync($"http://userprofileservice:8080/api/skills/only-if-exists/{currentUserId}");

            if (mySkillResponse.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"[Matching] Kullanıcının hiç skilli yok: {currentUserId}");
                return new List<MatchingDto>();
            }
            else if (!mySkillResponse.IsSuccessStatusCode)
            {
                var body = await mySkillResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"[Matching] Skill servisi başarısız. Status: {mySkillResponse.StatusCode}, Body: {body}");
                throw new Exception("Skill servisi başarısız.");
                
            }

            var mySkills = await mySkillResponse.Content.ReadFromJsonAsync<List<UserSkillDto>>();
            if (mySkills == null || !mySkills.Any())
            {
                Console.WriteLine($"[Matching] Skill listesi boş: {currentUserId}");
                return new List<MatchingDto>();
            }

            var wanted = mySkills
                .Where(s => s.SkillType == "Wanted")
                .Select(s => s.SkillName)
                .ToList();

            var idResponse = await client.GetAsync("http://authservice:8080/api/auth/all-ids");
            if (!idResponse.IsSuccessStatusCode)
                throw new Exception("Kullanıcı listesi alınamadı.");

            var allUserIds = await idResponse.Content.ReadFromJsonAsync<List<Guid>>();
            var result = new List<MatchingDto>();

            foreach (var otherId in allUserIds!)
            {
                if (otherId == currentUserId)
                    continue;

                var otherResponse = await client.GetAsync($"http://userprofileservice:8080/api/skills/only-if-exists/{otherId}");
                if (otherResponse.StatusCode == HttpStatusCode.NotFound)
                    continue;

                if (!otherResponse.IsSuccessStatusCode)
                    continue;

                var otherSkills = await otherResponse.Content.ReadFromJsonAsync<List<UserSkillDto>>();
                if (otherSkills == null)
                    continue;

                var known = otherSkills
                    .Where(s => s.SkillType == "Known")
                    .Select(s => s.SkillName)
                    .ToList();

                if (wanted.Intersect(known).Any())
                {
                    result.Add(new MatchingDto
                    {
                        MentorId = otherId,
                        MenteeId = currentUserId
                    });
                }
            }

            return result;
        }
    }
}
