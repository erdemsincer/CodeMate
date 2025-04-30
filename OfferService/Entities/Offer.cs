namespace OfferService.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }        // Teklifi veren
        public Guid CourseId { get; set; }      // Hangi kursa
        public decimal OfferedPrice { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
