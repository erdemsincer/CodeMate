namespace OfferService.Dtos
{
    public class OfferWithCourseDto
    {
        public int Id { get; set; }
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public decimal OfferedPrice { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
