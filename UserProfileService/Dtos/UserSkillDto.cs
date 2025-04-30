namespace UserProfileService.Dtos
{
    public class UserSkillDto
    {
       
        public string SkillName { get; set; } = null!;
        public string SkillType { get; set; } = null!; // "Known" veya "Wanted"
    }
}
