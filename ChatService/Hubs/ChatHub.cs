using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatService.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage( Guid receiverId, string content)
        {
            var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (senderId == null) return;
            // Belirli kullanıcıya mesaj gönder

            await Clients.Users(senderId, receiverId.ToString())
                         .SendAsync("ReceiveMessage", senderId, content);
        }
    }
}
