namespace OrderService.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }     // Teklifi onaylayan kişi
        public Guid CourseId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
