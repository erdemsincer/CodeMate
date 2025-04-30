using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Dtos;
using UserProfileService.Entities;

namespace UserProfileService.Services
{
    public class UserSkillService : IUserSkillService
    {
        private readonly UserProfileDbContext _context;

        public UserSkillService(UserProfileDbContext context)
        {
            _context = context;
        }

        public async Task AddSkillAsync(Guid userId, UserSkillDto dto)
        {
            var skill = new UserSkill
            {
                UserId = userId,
                SkillName = dto.SkillName,
                SkillType = Enum.Parse<SkillType>(dto.SkillType, true)
            };

            _context.UserSkills.Add(skill);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserSkillDto>> GetSkillsAsync(Guid userId)
        {
            return await _context.UserSkills
                .Where(s => s.UserId == userId)
                .Select(s => new UserSkillDto
                {
                    SkillName = s.SkillName,
                    SkillType = s.SkillType.ToString()
                })
                .ToListAsync();
        }

        public async Task DeleteSkillAsync(int skillId)
        {
            var skill = await _context.UserSkills.FindAsync(skillId);
            if (skill is not null)
            {
                _context.UserSkills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }
    }
}
