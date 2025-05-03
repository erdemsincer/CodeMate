namespace ChatService.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;
    }
}
