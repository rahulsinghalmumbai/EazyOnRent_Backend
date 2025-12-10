using EazyOnRent.Data;
using EazyOnRent.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EazyOnRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly EazyOnRentContext _context;

        public ConnectionController(EazyOnRentContext context)
        {
            _context = context;
        }

        // Save a new message (Sent)
        [HttpPost("save")]
        public async Task<IActionResult> SaveMessage([FromBody] ChatMessage chat)
        {
            if (chat == null) return BadRequest("Invalid message");

            chat.SentAt = DateTime.Now;
            chat.Status = MessageStatus.Sent;

            _context.ChatMessages.Add(chat);
            await _context.SaveChangesAsync();

            return Ok(chat);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetChatHistory(string user1, string user2)
        {
            if (string.IsNullOrWhiteSpace(user1) || string.IsNullOrWhiteSpace(user2))
                return BadRequest("Invalid user IDs");

            var chats = await _context.ChatMessages
                .Where(c => (c.SenderId == user1 && c.ReceiverId == user2) ||
                            (c.SenderId == user2 && c.ReceiverId == user1))
                .OrderBy(c => c.SentAt)
                .ToListAsync();

            return Ok(chats);
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateMessageStatus(int messageId, MessageStatus status)
        {
            var message = await _context.ChatMessages.FindAsync(messageId);
            if (message == null) return NotFound("Message not found");

            message.Status = status;

            if (status == MessageStatus.Delivered)
                message.DeliveredAt = DateTime.Now;
            else if (status == MessageStatus.Read)
                message.ReadAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(message);
        }
    }
}
