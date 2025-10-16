namespace EazyOnRent.Model.API_Model
{
    public class ListerItemResult
    {
        public ListerItemResult()
        {
            ItemImageList = new List<ItemImageResult>();
            Review = new List<string>();
        }
        public int ListerItemId { get; set; }
        public string? ItemName { get; set; }
        public string? CompanyName { get; set; }
        public int ListerId { get; set; }
        public int? bookCount { get; set; }
        public List<string>? Review { get; set; }
        public double? StarRating { get; set; }
        public int? viewCount { get; set; }
        public decimal? ItemCost { get; set; }
        public string? ItemDescriptions { get; set; }
        public DateTime? Availablefrom { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CategoryId { get; set; }
        
        public List<ItemImageResult> ItemImageList { get; set; }
    }
}
