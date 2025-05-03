namespace CodeMate.Web.Models
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public Guid CourseId { get; set; }
        public decimal OfferedPrice { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
