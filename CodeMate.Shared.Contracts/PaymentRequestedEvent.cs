namespace CodeMate.Shared.Contracts
{
    public class PaymentRequestedEvent
    {
        public int OrderId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
