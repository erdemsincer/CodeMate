namespace ChatService.Dtos
{
    public class SendMessageDto
    {
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = null!;
    }
}
