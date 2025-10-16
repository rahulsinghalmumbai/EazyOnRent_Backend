namespace EazyOnRent.Model.API_Model
{
    public class ItemResult
    {
        public int ListerItemId { get; set; }
        public string? ItemName { get; set; }
        public int ListerId { get; set; }
        public decimal ItemCost { get; set; }
        public string? ItemDescriptions { get; set; }
        public DateOnly Availablefrom { get; set; }
        public int CountOfImage { get; set; }
        public int BookedItem { get; set; }
        public string? Review { get; set; }
        public double Rating { get; set; }

    }
}
