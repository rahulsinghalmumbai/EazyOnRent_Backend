using Microsoft.AspNetCore.Http;

namespace EazyOnRent.Model.Dtos
{
    public class ItemImageResult
    {
        public int? ImageId { get; set; }
        public int? ListerItemId { get; set; }
        public string? ImageName { get; set; }
        public List<IFormFile>? ImageFiles { get; set; }
    }
}
