

using DoonGPay.Inteface;

namespace DoonGPay.Service
{
    public class ImageSaverService(IWebHostEnvironment env) : IImageSaverService
    {
        public string SaveImage(IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                    return ""; // رشته خالی در صورت نداشتن فایل

                string uploadFolder = Path.Combine(env.WebRootPath, "Image");

                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream); // بدون await
                }

                return $"/Image/{fileName}";
            }
            catch
            {
                return ""; // در صورت خطا هم رشته خالی برمی‌گردد
            }
        }
    }
}
