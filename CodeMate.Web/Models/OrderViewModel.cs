namespace CodeMate.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }
    }
}
