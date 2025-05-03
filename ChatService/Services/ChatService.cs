using ChatService.Data;
using ChatService.Dtos;
using ChatService.Entities;
using ChatService.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(ChatDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(Guid senderId, ChatMessageDto dto)
        {
            var message = new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            // ✅ SignalR ile karşı tarafa anlık mesaj gönder
            await _hubContext.Clients.User(dto.ReceiverId.ToString())
                .SendAsync("ReceiveMessage", new
                {
                    SenderId = senderId,
                    Content = dto.Content,
                    SentAt = message.SentAt
                });
        }


        public async Task<List<ChatMessage>> GetChatHistoryAsync(Guid user1Id, Guid user2Id)
        {
            return await _context.ChatMessages
                .Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                            (m.SenderId == user2Id && m.ReceiverId == user1Id))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}
