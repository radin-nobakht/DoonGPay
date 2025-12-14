using Azure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DoonGPay.Helpers
{
    public class ConvertIdToCookie
    {
        public static void AddCookie(HttpResponse response, string value, int? expireDays = 7)
        {
            // حذف کوکی قبلی اگر وجود دارد
            response.Cookies.Delete("TravelId");

            // اضافه کردن کوکی جدید
            var options = new CookieOptions
            {
                Path = "/",
                Expires = DateTime.Now.AddDays(expireDays ?? 7),
                HttpOnly = false, // اگر می‌خوای JS هم بخواند
                Secure = false    // اگر HTTPS داری true کن
            };
            response.Cookies.Append("TravelId", value, options);
        }

        // خواندن کوکی
        public static string GetCookie(HttpRequest request)
        {
            return request.Cookies["TravelId"];
        }
}
    }

