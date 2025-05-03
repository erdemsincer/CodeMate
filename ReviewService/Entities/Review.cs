namespace ReviewService.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public Guid ReviewerId { get; set; } // Yorumu yapan kullanıcı
        public Guid RevieweeId { get; set; } // Yorumu alan kullanıcı
        public string Content { get; set; } = null!;
        public int Rating { get; set; } // 1 - 5
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
