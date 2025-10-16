namespace EazyOnRent.Model.API_Model
{
    public class ListerModel
    {
        public int ListerId { get; set; }
        public string? CompanyName { get; set; }
        public string? Tags { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
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
        public string? ImageBase64Str { get; set; }
        public string? ImageFileExtn { get; set; }
    }
}
