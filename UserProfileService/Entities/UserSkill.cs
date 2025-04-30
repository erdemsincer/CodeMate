namespace UserProfileService.Entities
{
    public class UserSkill
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string SkillName { get; set; } = null!;
        public SkillType SkillType { get; set; }

        public User User { get; set; } = null!;
    }

    public enum SkillType
    {
        Known,   // Bildiği
        Wanted   // Öğrenmek istediği
    }
}
