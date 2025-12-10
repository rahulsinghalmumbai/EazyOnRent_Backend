namespace EazyOnRent.Model.Dtos
{
    public class ListerProfileDto
    {
        public int ListerId { get; set; }
        public string? CompanyName { get; set; }
        public string? Tags { get; set; }
        public string? Address { get; set; }

        public readonly string? Mobile;
        public string? Email { get; set; }
        public string? DefaultImage { get; set; }
        public string? Descriptions { get; set; }
        public bool? Status { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? City { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public string? LatLongAddress { get; set; }


        public IFormFile? ImageFile { get; set; }
    }
}
