using System.ComponentModel.DataAnnotations;

namespace EazyOnRent.Model
{
    public class ItemImage
    {
        [Key]
        public int? ImageId { get; set; }
        public int? ListerItemId { get; set; }
        public string? ImageName { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
