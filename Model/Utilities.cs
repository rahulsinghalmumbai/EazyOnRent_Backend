namespace EazyOnRent.Model
{
    public static class Utilities
    {
        public static string GetBase64String(string? path)
        {
            string base64ImageRepresentation = string.Empty;
            string FilePath = AppConfigModel.ImagePath + path;
            
            if (System.IO.File.Exists(FilePath))
            {
                byte[] imageByteData = System.IO.File.ReadAllBytes(FilePath);
                base64ImageRepresentation = Convert.ToBase64String(imageByteData);
            }
            return base64ImageRepresentation;
        }
    }
}
