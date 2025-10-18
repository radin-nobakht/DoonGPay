

using DoonGPay.Adapter;
using DoonGPay.Inteface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static System.Net.Mime.MediaTypeNames;

namespace DoonGPay.Service
{
    public class ImageService(IWebHostEnvironment env,MyContext db,IMySession mySession) : IImageService
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

        public FileContentResult ConvertToBase64()
        {
            var userId = mySession.UserId;

            var image =db.Users.Where(x=> x.Id==userId).Select(x=> x.TravelImage).FirstOrDefault();

            if (image == null)
                return null;

            // برگرداندن byte[] به عنوان فایل JPG
            return new FileContentResult(image, "image/jpeg");
        }

    }
}
