using ChatService.Dtos;
using ChatService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        // ✅ Giriş yapan kullanıcı mesaj gönderir
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto dto)
        {
            var senderId = GetUserIdFromToken();
            if (senderId == null)
                return Unauthorized();

            await _chatService.SendMessageAsync(senderId.Value, dto);
            return Ok(new { message = "Message sent." });
        }

        // ✅ Giriş yapan kullanıcı, biriyle olan geçmişi görür
        [HttpGet("history/{receiverId}")]
        public async Task<IActionResult> GetChatHistory(Guid receiverId)
        {
            var senderId = GetUserIdFromToken();
            if (senderId == null)
                return Unauthorized();

            var history = await _chatService.GetChatHistoryAsync(senderId.Value, receiverId);
            return Ok(history);
        }

        private Guid? GetUserIdFromToken()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier); // JWT'deki sub/id
            return Guid.TryParse(userIdStr, out var userId) ? userId : null;
        }
    }
}
