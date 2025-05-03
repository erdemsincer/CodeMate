using ChatService.Dtos;
using ChatService.Entities;

namespace ChatService.Services
{
    public interface IChatService
    {
        Task SendMessageAsync(Guid senderId, ChatMessageDto dto);
        Task<List<ChatMessage>> GetChatHistoryAsync(Guid user1Id, Guid user2Id);
    }
}
