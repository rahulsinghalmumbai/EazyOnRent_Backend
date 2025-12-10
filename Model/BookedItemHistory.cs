namespace EazyOnRent.Model
{
    public class BookedItemHistory
    {
        public int RenterItemId { get; set; }
        public int? RenterId { get; set; }
        public int? ItemId { get; set; }

        public DateTime? RentFromDate { get; set; }
        public DateTime? RentToDate { get; set; }
        public bool? BookingStatus { get; set; }

        public string? ItemName { get; set; }
        public decimal? ItemCost { get; set; }
        public string? ItemDescriptions { get; set; }

        public int? ListerId { get; set; }
        public string? ListerName { get; set; }
        public string? CompanyName { get; set; }

        public List<string>? ItemImages { get; set; } = new List<string>();
    }
}
