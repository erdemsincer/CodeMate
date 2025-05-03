using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Guid senderId, Guid receiverId, string content)
        {
            // Belirli kullanıcıya mesaj gönder
            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId, content);
        }
    }
}
