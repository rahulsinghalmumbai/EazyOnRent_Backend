namespace EazyOnRent.Model
{
    public class ChatRoom
    {
        public int ChatRoomId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int? ItemId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
