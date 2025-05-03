namespace ChatService.Dtos
{
    public class ChatMessageDto
    {
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = null!;
    }
}
