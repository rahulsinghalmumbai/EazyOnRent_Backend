using System.ComponentModel.DataAnnotations;

namespace EazyOnRent.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Mobile { get; set; }
        public string? CustomerType { get; set; }
        public float? Lat { get; set; }
        public float? Long { get; set; }
        public string? LatLongAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
