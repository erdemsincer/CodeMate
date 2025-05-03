namespace ReviewService.Dtos
{
    public class ReviewCreateDto
    {
        public Guid RevieweeId { get; set; }
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
    }
}
