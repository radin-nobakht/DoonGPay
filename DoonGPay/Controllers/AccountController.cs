using DoonGPay.Dto;
using DoonGPay.Inteface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
    {
        [Route("Login")]
        [HttpGet]
        public IActionResult Login() => View("LoginOtp");
        [HttpPost]
        public JsonResult LoginOtp(LoginDto login) => Json(accountService.LoginOtp(login));
        //[HttpPost]
        //public JsonResult LoginSendCode(string phoneNumber) => Json(accountService.LoginSendCode(phoneNumber));
        public async Task<IActionResult> Logout()
        {
            // حذف کوکی احراز هویت
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // ریدایرکت به صفحه لاگین
            return RedirectToAction("Login", "Account");
        }
    }
}
