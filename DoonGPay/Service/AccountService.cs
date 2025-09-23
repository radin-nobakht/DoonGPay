using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Helpers;
using DoonGPay.Inteface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DoonGPay.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DoonGPay.Service
{
    public class AccountService(MyContext db, IHttpContextAccessor httpContextAccessor) : IAccountService
    {      
public IBaseResult LoginOtp(LoginDto login)
    {
        // پیدا کردن کاربر بر اساس UserName
        var user = db.Users.FirstOrDefault(x => x.UserName == login.UserName);

        if (user == null)
            return new BaseResult(false, "کاربر پیدا نشد");

        // مقایسه پسورد هش شده
        if (!PasswordHelper.VerifyPassword(login.Password, user.Password))
            return new BaseResult(false, "رمز عبور اشتباه است");

        //// اعتبارسنجی OTP
        //var result = smsService.ValidateSms(login.Code, SmsType.Login);
        //if (!result.Result)
        //    return result;

        // ایجاد Claims و لاگین
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim("LastName", user.LastName),
        new Claim("FirstName", user.FristName),
        new Claim("UserName", user.UserName)
    };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        };

        httpContextAccessor.HttpContext?.SignInAsync(principal, properties);

        return new BaseResult(true);
    }

    //public IBaseResult LoginSendCode(string phoneNumber)
    //    {
    //        return smsService.SendSms(phoneNumber, SmsType.Login);
    //    }
    }
}
