using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Inteface
{
    public interface IImageService
    {
        string SaveImage(IFormFile image);
        FileContentResult ConvertToBase64();
    }
}