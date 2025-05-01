namespace CodeMate.Shared.Contracts
{
    public class PaymentCompletedEvent
    {
        public int OrderId { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
