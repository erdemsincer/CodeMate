namespace OrderService.Models
{
    public class OrderWithCourseDto
    {
        public int Id { get; set; }
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = "Bilinmiyor";
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }
    }
}
