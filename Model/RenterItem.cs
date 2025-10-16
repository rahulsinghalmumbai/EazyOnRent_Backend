
using System.ComponentModel.DataAnnotations;

namespace EazyOnRent.Model
{
    public class RenterItem
    {
        [Key]
        public int RenterItemId { get; set; }
        public int? RenterId { get; set; }
        public double? Rating { get; set; }
        public string? Review { get; set; }
        public int? ItemId { get; set; }
        public DateTime? RentFromDate { get; set; }
        public DateTime? RentToDate { get; set; }
        public bool? Status { get; set; }

    }
}
