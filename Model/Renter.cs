using System.ComponentModel.DataAnnotations;

namespace EazyOnRent.Model
{
    public class Renter
    {
        [Key]
        public int RenterId { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public string? LatLongAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Status { get; set; }
        public string? DefaultImage { get; set; }

       
    }
}
