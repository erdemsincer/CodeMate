namespace CodeMate.Web.Models
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
