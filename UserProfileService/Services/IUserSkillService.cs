using UserProfileService.Dtos;

namespace UserProfileService.Services
{
    public interface IUserSkillService
    {
        Task AddSkillAsync(UserSkillDto dto);
        Task<List<UserSkillDto>> GetSkillsAsync(Guid userId);
        Task DeleteSkillAsync(int skillId);
    }
}
