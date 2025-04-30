namespace OrderService.Events
{
    public class OfferApprovedEvent
    {
        public int OfferId { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal OfferedPrice { get; set; }
    }
}
