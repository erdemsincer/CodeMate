namespace MatchingService.Entities
{
    public class Matching
    {
        public int Id { get; set; }
        public Guid MentorId { get; set; } 
        public Guid MenteeId { get; set; } 
        public DateTime MatchedAt { get; set; }
    }
}
