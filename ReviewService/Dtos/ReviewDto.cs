namespace ReviewService.Dtos
{
    public class ReviewDto
    {
        public Guid ReviewerId { get; set; }
        public Guid RevieweeId { get; set; }
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
