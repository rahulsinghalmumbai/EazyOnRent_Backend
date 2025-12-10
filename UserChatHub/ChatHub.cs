//using EazyOnRent.Model;
//using Microsoft.AspNetCore.SignalR;

//namespace EazyOnRent.UserChatHub
//{
//    public class ChatHub :  Hub
//    {
//        public async Task SendMessage(string senderId, string receiverId, string message)
//        {
//            var chatMessage = new ChatMessage
//            {
//                SenderId = senderId,
//                ReceiverId = receiverId,
//                Message = message,
//                SentAt = DateTime.Now
//            };

//            // Broadcast to receiver
//            await Clients.User(receiverId).SendAsync("ReceiveMessage", chatMessage);

//            // Broadcast to sender (for confirmation)
//            await Clients.User(senderId).SendAsync("ReceiveMessage", chatMessage);
//        }
//    }
//}
using EazyOnRent.Data;
using EazyOnRent.Model;
using Microsoft.AspNetCore.SignalR;

namespace EazyOnRent.UserChatHub
{
    public class ChatHub : Hub
    {
        private readonly EazyOnRentContext _context;
        private static readonly Dictionary<string, string> _connectedUsers = new(); 

        public ChatHub(EazyOnRentContext context)
        {
            _context = context;
        }

        // Called when a user connects
        public async Task JoinChat(string userId)
        {
            if (!_connectedUsers.ContainsKey(Context.ConnectionId))
                _connectedUsers.Add(Context.ConnectionId, userId);

            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await Clients.Caller.SendAsync("SystemMessage", $"✅ Connected as {userId}");
        }

        // Send message
        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            var chatMessage = new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = message,
                SentAt = DateTime.Now,
                Status = MessageStatus.Sent
            };

            // Save in DB
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Update status to Delivered
            chatMessage.Status = MessageStatus.Delivered;
            chatMessage.DeliveredAt = DateTime.Now;
            await _context.SaveChangesAsync();

            // Send to receiver
            await Clients.Group(receiverId).SendAsync("ReceiveMessage", chatMessage);

            // Also send to sender
            await Clients.Group(senderId).SendAsync("ReceiveMessage", chatMessage);
        }

        // Mark message as Read
        public async Task MarkMessageAsRead(int messageId)
        {
            var message = await _context.ChatMessages.FindAsync(messageId);
            if (message == null) return;

            message.Status = MessageStatus.Read;
            message.ReadAt = DateTime.Now;
            await _context.SaveChangesAsync();

            // Notify sender that message is read
            await Clients.Group(message.SenderId).SendAsync("MessageRead", message);
        }

        // Disconnect
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connectedUsers.TryGetValue(Context.ConnectionId, out var userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
                _connectedUsers.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
