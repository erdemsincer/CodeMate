namespace UserProfileService.Dtos
{
    public class UserSkillDto
    {
        public Guid UserId { get; set; }
        public string SkillName { get; set; } = null!;
        public string SkillType { get; set; } = null!; // "Known" veya "Wanted"
    }
}
