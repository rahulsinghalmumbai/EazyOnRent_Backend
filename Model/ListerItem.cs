using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EazyOnRent.Model
{
    public class ListerItem
    {

        [Key]
        public int ListerItemId { get; set; }
        public string? ItemName { get; set; }
        public int ListerId { get; set; }

        [JsonIgnore]
        public Lister? Lister { get; set; }
        public decimal? ItemCost { get; set; }
        public string? ItemDescriptions { get; set; }
        public DateTime? Availablefrom { get; set; }
        public int? Status { get; set; }
        public bool? AvailabilityType { get; set; } = true;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CategoryId { get; set; }
    }
}
