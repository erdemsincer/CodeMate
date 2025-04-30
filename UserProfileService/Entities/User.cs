namespace UserProfileService.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    }
}
