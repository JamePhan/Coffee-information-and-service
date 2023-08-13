namespace Back.Utilities
{
    public static class CheckFileType
    {
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.ToLower() != "image/jpg"
                && file.ContentType.ToLower() != "image/jpeg"
                && file.ContentType.ToLower() != "image/webp"
                && file.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".jpg"
                && Path.GetExtension(file.FileName).ToLower() != ".png"
                && Path.GetExtension(file.FileName).ToLower() != ".webp"
                && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            return true;
        }
    }
}